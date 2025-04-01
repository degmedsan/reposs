using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.SysInventory.EN
{
    public class DetalleCompra
    {
        [Key]
        public int Id { get; set; }

        public int IdCompra { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage ="La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage ="La cantidad debe set al menos: 1.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage ="El precio unitario es obligatorio. ")]
        [Column(TypeName ="decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage ="El subtotal es obligatorio. ")]
        [Range(0.01, 99999999.99, ErrorMessage = "El Subtotal debe ser mayor a 0.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }

        //Relacion con Compra  (cada detalle pertenece a una compra)
        public virtual Compra? Compra { get; set; }

        //Relacion con Producto (cada detalle esta asociado a un producto)
        public virtual Producto? Producto { get; set; }
    }
}
