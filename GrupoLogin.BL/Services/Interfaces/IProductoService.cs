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
        List<Producto> GetAllProductos();
    }
}
