using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebTorrent.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.RouteExistingFiles = true;

            routes.MapRoute(null, "login", new { controller = "Account", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute(null, "account/login", new { controller = "Account", action = "Login", id = UrlParameter.Optional });
            routes.MapRoute(null, "logout", new { controller = "Account", action = "LogOut", id = UrlParameter.Optional });
           

            routes.MapRoute("all", "{*all}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
