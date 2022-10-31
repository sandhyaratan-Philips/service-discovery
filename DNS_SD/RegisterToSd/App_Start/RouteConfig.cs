using System.Web.Mvc;
using System.Web.Routing;

namespace RegisterToSd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            // Web API routes
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Weather", action = "SomeActionMethod" }
            );
        }
    }
}

