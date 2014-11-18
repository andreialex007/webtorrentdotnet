using System.Web.Mvc;

namespace WebTorrent.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return File("/index.html", "text/html");
        }
    }
}
