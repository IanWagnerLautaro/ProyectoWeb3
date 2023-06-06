using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

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
            }, OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult SignOutGoogle()
        {
            var redirectUrl = Url.Action("Index", "Home");
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            }, OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action("Index", "Home");
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
            }, OpenIdConnectDefaults.AuthenticationScheme);
        }

        public IActionResult SignInGoogle()
        {
            var redirectUrl = Url.Action("Index", "Home");
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
            }, GoogleDefaults.AuthenticationScheme);
        }



    }
}