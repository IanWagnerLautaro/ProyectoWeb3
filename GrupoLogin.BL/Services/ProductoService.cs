using GrupoLogin.BL.Services.Interfaces;
using GrupoLogin.DATA.Model;
using GrupoLogin.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoLogin.BL.Services
{
    public class ProductoService : IProductoService
    {
        private IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }


        public List<Producto> GetAllProductos()
        {
           return _productoRepository.GetAllProductos();
        }
    }
}
