using GrupoLogin.BL.Services.Interfaces;
using GrupoLogin.DATA;
using GrupoLogin.DATA.Model;
using GrupoLogin.WEB.Models;
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
            ListaProductosViewModel model= new ListaProductosViewModel();
            model.Productos = _productoService.GetAllProductos();
            model.Rol = 1;

            return View("ListaProductos", model);
        }

        public IActionResult EditarProducto(int id) 
        {
            Producto producto = _productoService.ObtenerProductoPorId(id);

            return View(producto);
        }

        [HttpPost]
        public IActionResult EditarProducto(Producto producto)
        {
            _productoService.EditarProducto(producto, producto.Id);
            return RedirectToAction("ListaProductos");
        }
        public IActionResult EliminarProducto(int id)
        {
            _productoService.DeleteProducto(id);
            return RedirectToAction("ListaProductos");
        }

        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProducto(Producto producto)
        {
            _productoService.CrearProducto(producto);
            return View();
        }
    }
}
