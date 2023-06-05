using GrupoLogin.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoLogin.BL.Services.Interfaces
{
    public interface IProductoService
    {
        Producto ObtenerProductoPorId(int id);
        List<Producto> GetAllProductos();
        void CrearProducto(Producto producto);
        void EditarProducto(Producto producto, int id);
        void DeleteProducto(int id);
    }
}
