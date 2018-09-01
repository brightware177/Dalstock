using System.Web;
using System.Web.Optimization;

namespace Dalstock_WebApp_Mysql_identity_19_02
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/plugins/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/plugins/simple-line-icons/simple-line-icons.min.css",
                "~/Content/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/material_style.css",
                      "~/Content/animate_page.css",
                      "~/Content/site.css",
                      "~/Content/plugins.min.css",
                      "~/Content/responsive.css"));

            bundles.Add(new ScriptBundle("~/bundles/appLayout").Include(
                     "~/Scripts/app.js",
                     "~/Scripts/layout.js",
                     "~/Scripts/theme-color.js",
                     "~/Content/plugins/material/material.min.js",
                     "~/Scripts/animations.js"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                     "~/Scripts/custom.js"
                     ));
            bundles.Add(new StyleBundle("~/bundles/steps").Include(
                     "~/Content/plugins/steps/jquery.steps.css"
                     ));
            bundles.Add(new ScriptBundle("~/scripts/steps").Include(
                     "~/Scripts/jquery.steps.min.js"
                     ));
            bundles.Add(new ScriptBundle("~/scripts/stepsValidate").Include(
                     "~/Scripts/validate/jquery.validate.min.js"
                     ));
            //chosen
            bundles.Add(new StyleBundle("~/bundles/chosen").Include(
                     "~/Content/plugins/chosen/bootstrap-chosen.css"
                     ));
            bundles.Add(new ScriptBundle("~/scripts/chosen").Include(
                      "~/Scripts/chosen/chosen.jquery.js"));

            //select2
            bundles.Add(new StyleBundle("~/bundles/select2").Include(
                     "~/Content/plugins/select2/select2.min.css"
                     ));
            bundles.Add(new ScriptBundle("~/scripts/select2").Include(
                      "~/Scripts/select2/select2.full.min.js"));

            //Slimscroll
            bundles.Add(new ScriptBundle("~/bundles/slimscroll").Include(
                      "~/Content/plugins/jquery-slimscroll/jquery.slimscroll.min.js"));

            //Datatable
            bundles.Add(new ScriptBundle("~/scripts/datatable").Include(
                     "~/Content/plugins/datatables/jquery.dataTables.min.js",
                     "~/Content/plugins/datatables/plugins/bootstrap/dataTables.bootstrap4.min.js",
                     "~/Content/plugins/datatables/dataTables.buttons.min.js",
                     "~/Content/plugins/datatables/jszip.min.js",
                     "~/Content/plugins/datatables/pdfmake.min.js",
                     "~/Content/plugins/datatables/vfs_fonts.js",
                     "~/Content/plugins/datatables/buttons.html5.min.js",
                     "~/Scripts/table/table_data.js",
                     "~/Content/plugins/datatables/dataTables.responsive.min.js",
                     "~/Content/plugins/datatables/buttons.print.min.js"
                     ));
            bundles.Add(new StyleBundle("~/bundles/datatable").Include(
                      "~/Content/plugins/datatables/plugins/bootstrap/dataTables.bootstrap4.min.css"));
            //Popper
            bundles.Add(new ScriptBundle("~/scripts/popper").Include(
                     "~/Content/plugins/popper/popper.min.js"
                     ));

            //Sweet alert
            bundles.Add(new StyleBundle("~/bundles/sweet-alert").Include(
                      "~/Content/plugins/sweet-alert/sweetalert.min.css"));
            
            bundles.Add(new ScriptBundle("~/scripts/sweet-alert").Include(
                     "~/Content/plugins/sweet-alert/sweetalert.min.js",
                     "~/Scripts/sweet-alert/sweet-alert-data.js"
                     ));
            //checkbox
            bundles.Add(new StyleBundle("~/bundles/checkbox").Include(
                      "~/Content/formlayout.css"));
        }
    }
}
