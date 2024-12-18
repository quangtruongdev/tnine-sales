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

            // AngularJs
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/wwwroot/lib/angular/angular.js",
                "~/wwwroot/lib/angular-ui-router/release/angular-ui-router.js",
                "~/wwwroot/lib/oclazyload/dist/ocLazyLoad.js"
                ));

            // App
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/app/app.module.js",
                "~/app/app.route.js",
                "~/app/app.config.js",

                "~/app/services/service-proxy.module.js",
                "~/app/services/base-service.js",
                "~/app/services/service-proxies.js",
                "~/app/services/auth-service.js",
                "~/app/services/toastrService.js"
                ));

            // Common
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/app/shared/common/app-common.module.js",
                "~/app/shared/common/input-types/tnine-combobox/tnine-combobox.component.js",
                "~/app/shared/common/input-types/tnine-multiselect/tnine-multiselect.component.js",
                "~/app/shared/layout/layout.module.js",
                "~/app/shared/layout/sidebar/sidebar.component.js"
                ));
        }
    }
}
