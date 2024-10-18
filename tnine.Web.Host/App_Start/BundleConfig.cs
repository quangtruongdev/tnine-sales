using System.Web.Optimization;

namespace tnine.Web.Host
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Angular core
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/wwwroot/lib/angular/angular.js",
                "~/wwwroot/lib/angular-ui-router/release/angular-ui-router.js"
                ));

            // App
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/app/app.module.js",
                "~/app/app.config.js",
                "~/app/shared/common/tnine.common.js"
                ));

            // Home
            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                "~/app/pages/home/home.module.js",
                "~/app/pages/home/home.controller.js",
                "~/app/pages/home/home.service.js"
                ));

            // Todo
            bundles.Add(new ScriptBundle("~/bundles/todo").Include(
                "~/app/pages/todo/todo.module.js",
                "~/app/pages/todo/todo.controller.js",
                "~/app/pages/todo/todo.service.js"
                ));

            // UI
            bundles.Add(new ScriptBundle("~/bundles/ui").Include(
                "~/app/components/ui/ui.module.js",
                "~/app/components/ui/table/table.module.js"

                //"~/app/shared/ui/ui.directive.js"
                ));
        }
    }
}
