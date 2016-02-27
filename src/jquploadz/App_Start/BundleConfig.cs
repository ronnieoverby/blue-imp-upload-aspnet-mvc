using System.Web.Optimization;

namespace jquploadz
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/jqupload-styles").Include(
                "~/node_modules/blueimp-gallery/css/blueimp-gallery.css",
                "~/node_modules/blueimp-bootstrap-image-gallery/css/bootstrap-image-gallery.css",
                "~/node_modules/blueimp-file-upload/css/jquery.fileupload.css",
                "~/node_modules/blueimp-file-upload/css/jquery.fileupload-ui.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqupload").Include(
                "~/node_modules/blueimp-file-upload/js/vendor/jquery.ui.widget.js",
                "~/node_modules/blueimp-tmpl/js/tmpl.js",
                "~/node_modules/blueimp-load-image/js/load-image.all.min.js",
                "~/node_modules/blueimp-canvas-to-blob/js/canvas-to-blob.js",
				"~/node_modules/blueimp-gallery/js/blueimp-gallery.js",
                "~/node_modules/blueimp-gallery/js/jquery.blueimp-gallery.js",
                "~/node_modules/blueimp-bootstrap-image-gallery/js/bootstrap-image-gallery.js",
                "~/node_modules/blueimp-file-upload/js/jquery.iframe-transport.js",
                "~/node_modules/blueimp-file-upload/js/jquery.fileupload.js",
                "~/node_modules/blueimp-file-upload/js/jquery.fileupload-process.js",
                "~/node_modules/blueimp-file-upload/js/jquery.fileupload-image.js",
                "~/node_modules/blueimp-file-upload/js/jquery.fileupload-audio.js",
                "~/node_modules/blueimp-file-upload/js/jquery.fileupload-video.js",
                "~/node_modules/blueimp-file-upload/js/jquery.fileupload-validate.js",
                "~/node_modules/blueimp-file-upload/js/jquery.fileupload-ui.js",
                "~/Scripts/jqupload.main.js"
                ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/node_modules/font-awesome/css/font-awesome.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
