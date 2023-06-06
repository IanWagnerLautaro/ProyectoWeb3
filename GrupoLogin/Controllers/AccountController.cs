using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GrupoLogin.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme, "Google");
        }

        public IActionResult SignOutGoogle()
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action("Index", "Home");
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
            });
        }

        public IActionResult SignInGoogle()
        {
            var redirectUrl = Url.Action("Index", "Home");
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
            }, "Google");
        }



    }
}