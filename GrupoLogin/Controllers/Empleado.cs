using Microsoft.AspNetCore.Mvc;

namespace GrupoLogin.WEB.Controllers
{
    public class Empleado : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaProductos()
        {
            //ViewBag.ListaProductos = _productoService.GetAllProductos();
            return View();
        }
    }
}
