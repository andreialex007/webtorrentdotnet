using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebTorrent.WebApp.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            //            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //            routes.RouteExistingFiles = true;
            //
            //            routes.MapHttpRoute("Default Api", "api/{controller}/{id}");
            //
            //            routes.MapRoute("all", "{*all}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //            );
        }
    }
}
