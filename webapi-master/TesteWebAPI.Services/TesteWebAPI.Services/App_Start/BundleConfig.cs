using System.Web;
using System.Web.Optimization;

namespace TesteWebAPI.Services
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/jquery.inputmask/jquery.inputmask.js",
                      "~/Scripts/chosen.jquery.min.js",
                       "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/locales/bootstrap-datepicker.pt-BR.min.js",
                      "~/Scripts/bootstrap-datepicker-globalize.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/site.css",
                      "~/Content/chosen.min.css"));
        }
    }
}
