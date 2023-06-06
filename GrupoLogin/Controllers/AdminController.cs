using GrupoLogin.BL.Services.Interfaces;
using GrupoLogin.DATA;
using GrupoLogin.DATA.Model;
using GrupoLogin.DATA.Model.Enums;
using GrupoLogin.WEB.Models;
using Microsoft.AspNetCore.Identity;
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

        public IActionResult EditarProducto(int id) 
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                Producto producto = _productoService.ObtenerProductoPorId(id);
                return View(producto);
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult EditarProducto(Producto producto)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                _productoService.EditarProducto(producto, producto.Id);
                return RedirectToAction("ListaProductos","Producto");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public IActionResult EliminarProducto(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                _productoService.DeleteProducto(id);
                return RedirectToAction("ListaProductos", "Producto");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult RegistrarProducto()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult RegistrarProducto(Producto producto)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                _productoService.CrearProducto(producto);
                return RedirectToAction("ListaProductos", "Producto");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
