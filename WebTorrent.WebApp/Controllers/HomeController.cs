using System.Web.Mvc;
using WebTorrent.WebApp.Controllers.Common;

namespace WebTorrent.WebApp.Controllers
{
    [CustomAuthorize]
    public class HomeController : Common.ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
