using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTorrent.Domain.Exceptions;
using WebTorrent.Domain.Services.User;
using WebTorrent.WebApp.Controllers.Common;

namespace WebTorrent.WebApp.Controllers
{
    public class AccountController : Common.ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("IndexUnauthorized");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string name, string password, bool isPersistent = false)
        {
            var user = _userService.Login(name, password);
            this.SignIn(user, isPersistent);
            return SuccessJsonResult();
        }

        protected JsonResult SuccessJsonResult()
        {
            return Json(new { Result = "ok" });
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            this.SignOut();
            return Redirect(Url.Content("~/"));
        }
    }
}