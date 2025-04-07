using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.SysInventory.EN
{
    public class Cliente
    {
        [ Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(255, ErrorMessage = "El nombre no pude tener mas de 255 caracteres. ")]
        public string Nombre { get; set; }

        [StringLength(80, ErrorMessage = "La direccion no puede tener mas de 80 caracteres. ")]
        public string? Direccion { get; set; }

        [StringLength(20, ErrorMessage = "El telefono no puede tener mas de 20 caracteres. ")]
        public string? Telefono { get; set; }

        [StringLength(100, ErrorMessage = "El correo electronico no puede tener mas de 100 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato valido.")]
        public string? Email { get; set; }
    }
}
