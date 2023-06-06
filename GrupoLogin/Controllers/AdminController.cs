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
                TempData["Mensaje"] = (!User.IsInRole("Admin") && User.IsInRole("Usuario")) ? $"No tenes permisos para realizar esta accion" : $"Tenes que iniciar sesion para esta accion";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult EditarProducto(Producto producto)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                _productoService.EditarProducto(producto, producto.Id);
                TempData["Resultado"] = $"Se ha editado correctamente: {producto.Nombre}";
                return RedirectToAction("ListaProductos","Producto");
            }
            else
            {
                 TempData["Mensaje"] = (!User.IsInRole("Admin") && User.IsInRole("Usuario"))? $"No tenes permisos para realizar esta accion" : $"Tenes que iniciar sesion para esta accion";

                return RedirectToAction("Index", "Home");
            }

        }
        public IActionResult EliminarProducto(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                _productoService.DeleteProducto(id);
                TempData["Resultado"] = $"Se ha eliminado correctamente";
                return RedirectToAction("ListaProductos", "Producto");
            }
            else
            {
                TempData["Mensaje"] = (!User.IsInRole("Admin") && User.IsInRole("Usuario")) ? $"No tenes permisos para realizar esta accion" : $"Tenes que iniciar sesion para esta accion";

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
                TempData["Mensaje"] = (!User.IsInRole("Admin") && User.IsInRole("Usuario"))? $"No tenes permisos para realizar esta accion" : $"Tenes que iniciar sesion para esta accion";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult RegistrarProducto(Producto producto)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                _productoService.CrearProducto(producto);
                TempData["Resultado"] = $"Se ha registrado correctamente: {producto.Nombre}";
                return RedirectToAction("ListaProductos", "Producto");
            }
            else
            {
                TempData["Mensaje"] = (!User.IsInRole("Admin") && User.IsInRole("Usuario")) ? $"No tenes permisos para realizar esta accion" : $"Tenes que iniciar sesion para esta accion";

                return RedirectToAction("Index", "Home");
            }

        }
    }
}
