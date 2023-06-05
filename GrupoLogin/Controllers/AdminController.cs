using GrupoLogin.BL.Services.Interfaces;
using GrupoLogin.DATA;
using GrupoLogin.DATA.Model;
using Microsoft.AspNetCore.Mvc;

namespace GrupoLogin.WEB.Controllers
{
    public class AdminController : Controller
    {
        private IProductoService _productoService;
        private GrupoLoginContext _context;

        public AdminController(IProductoService productoService)
        {
            _productoService = productoService;
            _context = new GrupoLoginContext();
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
            _context.Producto.Add(producto);
            _context.SaveChanges();
            return View();
        }
    }
}
