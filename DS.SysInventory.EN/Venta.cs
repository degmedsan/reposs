using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.SysInventory.EN
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha de compra es obligatoria.")]
        public DateTime FechaVenta { get; set; }

        [Required(ErrorMessage = "El proveedor es obligatorio.")]
        [ForeignKey("Proveedor")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El total de la compra es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El total debe ser mayor a 0 y menor a 1,000,000. ")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public byte Estado { get; set; }

        //Relacion con Cliente
        public virtual Cliente? Cliente { get; set; }

        //Relacion con DetalleVenta  (una compra tiene varios detalles)
        public virtual ICollection<DetalleVenta>? DetalleVentas { get; set; }
        
        public enum EnumEstadoVenta
        {
            Activa = 1,
            Anulada = 2,
        }

    }
}
