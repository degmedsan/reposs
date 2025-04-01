using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.SysInventory.EN
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0, 20)]

        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public int CantidadDisponible { get; set; }

        [Required(ErrorMessage = "La fecha de creacion es obligatorio")]
        public DateTime FechaCreacion { get; set; }

    }
}
