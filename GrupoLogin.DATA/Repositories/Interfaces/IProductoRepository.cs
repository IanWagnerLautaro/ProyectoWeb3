using GrupoLogin.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoLogin.DATA.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        List<Producto> GetAllProductos();
    }
}
