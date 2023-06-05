using GrupoLogin.BL.Services.Interfaces;
using GrupoLogin.DATA.Model;
using Microsoft.AspNetCore.Mvc;

namespace GrupoLogin.WEB.Controllers
{
    public class AdminController : Controller
    {
        private IProductoService _productoService;

        public AdminController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaProductos()
        {
            //ViewBag.ListaProductos = _productoService.GetAllProductos();
            return View();
        }

        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProducto(Producto producto)
        {
            return View();
        }
    }
}
