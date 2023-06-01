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
    }
}
