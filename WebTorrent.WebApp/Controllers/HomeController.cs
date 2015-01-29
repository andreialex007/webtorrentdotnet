using System.Web.Mvc;
using WebTorrent.WebApp.Controllers.Common;

namespace WebTorrent.WebApp.Controllers
{
    public class HomeController : Common.ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
