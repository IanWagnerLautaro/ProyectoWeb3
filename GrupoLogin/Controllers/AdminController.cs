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

        public IActionResult ListaProductos()
        {
            if (User.Identity.IsAuthenticated)
            {
                ListaProductosViewModel model= new ListaProductosViewModel();
                model.Productos = _productoService.GetAllProductos();
                model.Rol = User.IsInRole("Admin") ? (int)Rol.Admin : (int)Rol.Usuario;

                return View("ListaProductos", model);
            }
            else
            {
                return RedirectToAction("Index");
            }
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
                return RedirectToAction("ListaProductos");
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
                return RedirectToAction("ListaProductos");
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
                return RedirectToAction("ListaProductos");
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
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
