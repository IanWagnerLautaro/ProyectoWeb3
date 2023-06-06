using GrupoLogin.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GrupoLogin.DATA.Model;
using GrupoLogin.DATA.Model.Enums;
using GrupoLogin.WEB.Models;
using Microsoft.AspNetCore.Identity;

namespace GrupoLogin.WEB.Controllers
{
    public class ProductoController : Controller
    {

        private IProductoService _productoService;

        public ProductoController(IProductoService productoService)
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
                ListaProductosViewModel model = new ListaProductosViewModel();
                model.Productos = _productoService.GetAllProductos();
                model.Rol = User.IsInRole("Admin") ? (int)Rol.Admin : (int)Rol.Usuario;

                return View("ListaProductos", model);
            }
            else
            {
                TempData["Mensaje"] = $"Tenes que iniciar sesion para esta accion";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
