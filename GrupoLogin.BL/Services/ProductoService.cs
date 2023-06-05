using GrupoLogin.BL.Services.Interfaces;
using GrupoLogin.DATA;
using GrupoLogin.DATA.Model;
using GrupoLogin.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GrupoLogin.BL.Services
{
    public class ProductoService : IProductoService
    {
        private IProductoRepository _productoRepository;
        private GrupoLoginContext _context;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
            _context = new GrupoLoginContext();
        }

        public void CrearProducto(Producto producto)
        {
            try
            {
                _context.Producto.Add(producto);
                _context.SaveChanges();
                
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public void DeleteProducto(int id)
        {
            Producto producto = ObtenerProductoPorId(id);

            if(producto != null) {
                _context.Remove(producto);
                _context.SaveChanges();
            }

        }

        public void EditarProducto(Producto producto, int id)
        {
            try
            {

                Producto p = ObtenerProductoPorId(id);
                if (p != null)
                {
                    p.Precio = producto.Precio;
                    p.Nombre = producto.Nombre;
                    p.Descripcion = producto.Descripcion;
                    p.cantidad = producto.cantidad;

                    _context.Update(p);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Producto> GetAllProductos()
        {
            return _context.Producto.ToList();
        }

        public Producto ObtenerProductoPorId(int id)
        {
            return _context.Producto.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
