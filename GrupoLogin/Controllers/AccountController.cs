using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace GrupoLogin.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> SignOutAsync()
        {
            //return SignOut(new AuthenticationProperties
            //{
            //    RedirectUri = Url.Action("Index", "Home")
            //}, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);

            var redirectUrl = Url.Action("Index", "Home");
            //return SignOut(new AuthenticationProperties
            //{
            //    RedirectUri = redirectUrl
            //}, "Google");

            await HttpContext.SignOutAsync();
            return Redirect(redirectUrl);
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

            //var redirectUrl = Url.Action("Index", "Home");
            //return Challenge(new AuthenticationProperties
            //{
            //    RedirectUri = redirectUrl,
            //}, GoogleDefaults.AuthenticationScheme);
        }

        public IActionResult SignInGoogle()
        {
            var redirectUrl = Url.Action("Index", "Home");
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
            }, GoogleDefaults.AuthenticationScheme);
        }

        public IActionResult SignInFacebook()
        {
            var redirectUrl = Url.Action("Index", "Home");
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
            }, FacebookDefaults.AuthenticationScheme);
        }

        public IActionResult SignInLinkedin()
        {
            var redirectUrl = Url.Action("Index", "Home");
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
            }, "LinkedIn");
        }



    }
}