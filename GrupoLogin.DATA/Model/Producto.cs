using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoLogin.DATA.Model
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe asignar un precio")]
        [Range(10, 1000,
        ErrorMessage = "El precio debe ser superior a cero")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Debe asignar un nombre")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debe asignar una descripción")]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "Debe asignar cantidad")]
        [Range(1, 2000000,
        ErrorMessage = "La cantidad no puede ser negativo")]
        public int cantidad { get; set; }

    }
}
