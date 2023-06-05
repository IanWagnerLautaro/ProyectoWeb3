using GrupoLogin.DATA.Model;

namespace GrupoLogin.WEB.Models
{
    public class ListaProductosViewModel
    {
        public List<Producto> Productos { get; set; }
        public int Rol { get; set; } = 0;
    }
}
