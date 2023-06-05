using GrupoLogin.DATA.Model.DTOS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrupoLogin.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginRegister() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuarioDTO usuario) 
        {
            return View();
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync();
            // Aquí puedes obtener la información del usuario autenticado con Facebook y realizar las acciones correspondientes, como crear una sesión de usuario local.

            // Redirigir a la página principal o a otra página deseada.
            return null;
        }
    }
}
