using System.Web.Mvc;

namespace WebTorrent.WebApp.Controllers.Common
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
