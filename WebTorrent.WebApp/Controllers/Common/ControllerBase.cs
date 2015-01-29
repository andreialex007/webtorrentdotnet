using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTorrent.WebApp.Controllers.Common
{
//    [CustomAuthorizeAttribute]
    public class ControllerBase : Controller
    {
        public ControllerBase()
        {
            ActionInvoker = new ExceptionControllerActionInvoker();
        }
    }
}