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

//            routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            //            routes.MapRoute(null, "api/torrents/downloading", new
            //                                                          {
            //                                                              controller = "Torrents",
            //                                                              action = "TorrentsWithState"
            //                                                          });
            //            routes.MapRoute("Default Api", "api/{controller}/{id}/{state}", new { controller = "Torrents", id = RouteParameter.Optional }, new { id = @"^\d*$" });


            routes.MapRoute("all", "{*all}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
