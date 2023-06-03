using GrupoLogin.DATA.Model.DTOS;
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
    }
}
