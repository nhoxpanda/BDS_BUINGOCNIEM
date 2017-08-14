using System.Web;
using System.Web.Optimization;

namespace PROJECTBDS
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

            bundles.Add(new ScriptBundle("~/bundles/header").Include(
                        "~/Content/plugins/jQuery/jquery-2.2.3.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/footer").Include(
                        "~/Content/plugins/datepicker/bootstrap-datepicker.js",
                        "~/Content/plugins/slimScroll/jquery.slimscroll.min.js",
                        "~/Content/plugins/fastclick/fastclick.js",
                        "~/Content/dist/js/app.js",
                        "~/Content/dist/js/pages/dashboard.js",
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/jquery.dataTables.columnFilter.js",
                        "~/Content/plugins/select2/select2.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/homepage").Include(
               "~/Content/assets/css/bootstrap.min.css",
               "~/Content/assets/css/owl.carousel.css",
               "~/Content/assets/css/owl.theme.css",
               "~/Content/assets/css/fonts-css.css",
               "~/Content/assets/css/style.css",
               "~/Content/assets/css/responsive.css",
               "~/Content/assets/css/custom.css",
               "~/Content/toastr8-master/css/toastr8.min.css"
               ));


            bundles.Add(new StyleBundle("~/Content/Admin").Include(
                        "~/Content/bootstrap/css/bootstrap.css",
                        "~/Content/font-awesome-4.4.0/css/font-awesome.min.css",
                        "~/Content/dist/css/AdminLTE.min.css",
                        "~/Content/dist/css/skins/_all-skins.min.css",
                        "~/Content/plugins/morris/morris.css",
                        "~/Content/plugins/datepicker/datepicker3.css",
                        "~/Content/plugins/daterangepicker/daterangepicker.css",
                        "~/Content/plugins/datatables/jquery.dataTables.min.css",
                        "~/Content/plugins/select2/select2.min.css"));
        }
    }
}
