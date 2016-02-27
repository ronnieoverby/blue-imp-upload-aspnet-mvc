using System.IO;
using System.Web;
using System.Web.Mvc;

namespace jquploadz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadForm()
        {
            return View();
        }

        public ActionResult Upload(HttpPostedFileBase[] files)
        {
            // save em or whatever

            foreach (var file in files)
            {
                SaveFileToDisk(file);
            }


            
        }

        private static void SaveFileToDisk(HttpPostedFileBase file)
        {
            var targetPath = Path.Combine(@"C:\Users\rdodo\Desktop\uploaded files", file.FileName);
            file.SaveAs(targetPath);
        }
    }
}