using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoStorage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            //routes.MapRoute(
            //    name: "Gallery",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new {controller = "Galleries", action = "Index", id = UrlParameter.Optional}
            //    );
        }
    }
}
