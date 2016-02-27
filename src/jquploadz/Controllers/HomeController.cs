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
        private const int ThumbSize = 120;

        public ActionResult Index()
        {
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
            if (!contentType.StartsWith("image"))
                return Redirect($"https://placehold.it/{ThumbSize}?text={Url.Encode(file.Extension)}");

            using (var img = Image.FromFile(file.FullName))
            using (var thumb = img.GetThumbnailImage(ThumbSize, ThumbSize, () => false, IntPtr.Zero))
            {
                var ms = new MemoryStream();
                thumb.Save(ms, img.RawFormat);
                ms.Position = 0;
                return File(ms, contentType);
            }
        }

        [HttpPost]
        public ActionResult DeleteFile(string name)
        {
            var file = GetFile(name);
            file.Delete();
            return Json($"{name} was deleted");
        }

        [HttpGet]
        public ActionResult Upload(string name = null)
        {
            var folder = GetUploadFolder();

            var files = from file in folder.GetFiles()
                        where name == null || name.Equals(file.Name, StringComparison.OrdinalIgnoreCase)
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
        public ActionResult Upload(HttpPostedFileBase file)
        {
            SaveFileToDisk(file);
            return Upload(file.FileName);
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