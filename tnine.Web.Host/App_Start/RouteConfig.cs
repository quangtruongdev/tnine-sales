using System.Web.Mvc;
using System.Web.Routing;

namespace tnine.Web.Host
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("wwwroot/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Angular",
                url: "{*url}",
                defaults: new { controller = "Angular", action = "Index" }
            );
        }
    }
}
