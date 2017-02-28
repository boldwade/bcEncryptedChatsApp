using System.Web.Mvc;
using System.Web.Routing;

namespace WebAPIForBoldchat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "UtilDefault",
            //    url: "util/v1/{input}",
            //    defaults: new { controller = "Util", action = "Index", input = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default2",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
