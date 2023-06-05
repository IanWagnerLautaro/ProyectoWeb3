using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoLogin.DATA.Model
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public double Precio { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int cantidad { get; set; }

    }
}
