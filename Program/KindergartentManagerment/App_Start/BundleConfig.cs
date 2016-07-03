using System.Web;
using System.Web.Optimization;

namespace KindergartentManagerment
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/jquerycore").Include(
                        "~/Content/themes/AdminLTE/js/jQuery/jQuery-2.1.4.min.js", //jQuery 2.1.4
                        "~/Content/bootstrap/js/bootstrap.min.js",  //Bootstrap 3.3.5
                        "~/Content/themes/AdminLTE/js/app.min.js", //AdminLTE App
                        "~/Content/themes/AdminLTE/js/demo.js"  //AdminLTE for demo purposes
                        ));

            bundles.Add(new StyleBundle("~/bundles/csscore").Include(
                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/themes/AdminLTE/css/font-awesome.min.css",  //<!-- Font Awesome -->
                      "~/Content/themes/AdminLTE/css/ionicons.min.css",
                      "~/Content/themes/AdminLTE/css/AdminLTE.min.css",
                      "~/Content/themes/AdminLTE/css/skins/_all-skins.min.css" //AdminLTE Skins.Choose a skin from the css/ skins folder instead of downloading all of them to reduce the load. 
                      ));
            bundles.Add(new StyleBundle("~/bundles/cssother").Include(
                    "~/Content/themes/AdminLTE/css/iCheck/square/blue.css"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryother").Include(
                     "~/Content/themes/AdminLTE/js/input-mask/jquery.inputmask.js",
                     "~/Content/themes/AdminLTE/js/input-mask/jquery.inputmask.date.extensions.js",
                     "~/Content/themes/AdminLTE/js/input-mask/jquery.inputmask.extensions.js",
                     "~/Content/themes/AdminLTE/js/iCheck/icheck.min.js",
                     "~/Content/themes/AdminLTE/js/slimScroll/jquery.slimscroll.min.js",
                     "~/Content/themes/AdminLTE/js/fastclick/fastclick.min.js"
                    ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
