using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebTorrent.Domain.Services.User;

namespace WebTorrent.WebApp.Controllers.Common
{
    public static class AuthentificationExtensions
    {
        public const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";

        public static void SignIn(this ControllerBase controllerBase, UserDto user, bool isPersistent = false)
        {
            controllerBase.SignIn(user.Name, isPersistent, new[] { user.Role });
        }

        public static void SignIn(this ControllerBase controllerBase, string userName, bool isPersistent = false, params string[] roles)
        {
            var authenticationManager = controllerBase.HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignIn();
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Name, userName, StringValueType));

            foreach (var role in roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, role, StringValueType));

            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        public static void SignOut(this ControllerBase controllerBase)
        {
            var authenticationManager = controllerBase.HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }
    }
}