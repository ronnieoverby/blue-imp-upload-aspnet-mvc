using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public ActionResult Upload(HttpPostedFileBase file)
        {

             

            // save em or whatever
            SaveFileToDisk(file);

            return Content($"got 1 files!");
        }

        private static void SaveFileToDisk(HttpPostedFileBase file)
        {
            var targetPath = Path.Combine(@"C:\Users\rdodo\Desktop\uploaded files", Guid.NewGuid().ToString());
            file.SaveAs(targetPath);
        }
    }
}