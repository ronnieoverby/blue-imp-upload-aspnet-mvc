using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jquploadz.Controllers
{
    public class HomeController : Controller
    {
        private const int ThumbSize = 160;

        public ActionResult Index()
        {
            ViewBag.ThumbSize = ThumbSize;
            return View();
        }

        public ActionResult GetFile(string name, bool thumbnail = false)
        {
            var file = GetFile(name);
            var contentType = MimeMapping.GetMimeMapping(file.Name);

            return thumbnail
                ? Thumb(file, contentType)
                : File(file.FullName, contentType);
        }

        private ActionResult Thumb(FileInfo file, string contentType)
        {
            if (contentType.StartsWith("image"))
            {
                try
                {
                    using (var img = Image.FromFile(file.FullName))
                    {
                        var thumbHeight = (int) (img.Height*(ThumbSize/(double) img.Width));

                        using (var thumb = img.GetThumbnailImage(ThumbSize, thumbHeight, () => false, IntPtr.Zero))
                        {
                            var ms = new MemoryStream();
                            thumb.Save(ms, img.RawFormat);
                            ms.Position = 0;
                            return File(ms, contentType);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // todo log exception
                }
            }

            return Redirect($"https://placehold.it/{ThumbSize}?text={Url.Encode(file.Extension)}");
        }

        [HttpPost]
        public ActionResult DeleteFile(string name)
        {
            var file = GetFile(name);
            file.Delete();
            return Json($"{name} was deleted");
        }

        [HttpGet]
        public ActionResult Upload(IEnumerable<string> names = null)
        {
            var folder = GetUploadFolder();

            var files = from file in folder.GetFiles()
                        where names == null || names.Contains(file.Name, StringComparer.OrdinalIgnoreCase)
                        select new
                        {
                            deleteType = "POST",
                            name = file.Name,
                            size = file.Length,
                            url = Url.Action("GetFile", "Home", new { file.Name }),
                            thumbnailUrl = Url.Action("GetFile", "Home", new { file.Name, thumbnail = true }),
                            deleteUrl = Url.Action("DeleteFile", "Home", new { file.Name }),
                        };

            return Json(new
            {
                files
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Upload()
        {
            var files = Request.Files
                .Cast<string>()
                .Select(k => Request.Files[k])
                .ToArray();

            foreach (var file in files)
                SaveFileToDisk(file);

            var names = files.Select(f => f.FileName);
            return Upload(names);
        }

        private static void SaveFileToDisk(HttpPostedFileBase file)
        {
            var folder = GetUploadFolder();
            var targetPath = Path.Combine(folder.FullName, file.FileName);
            file.SaveAs(targetPath);
        }

        private static DirectoryInfo GetUploadFolder()
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var upload = Path.Combine(desktop, "uploaded files");
            var di = new DirectoryInfo(upload);

            if (!di.Exists)
                di.Create();

            return di;
        }

        private static FileInfo GetFile(string name)
        {
            var folder = GetUploadFolder();
            var file = folder.GetFiles(name).Single();
            return file;
        }
    }
}