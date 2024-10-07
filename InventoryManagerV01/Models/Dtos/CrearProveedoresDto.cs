using System.ComponentModel.DataAnnotations;

namespace InventoryManagerV01.Models.Dtos
{
    public class CrearProveedoresDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "El nombre del proveedor no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [StringLength(255, ErrorMessage = "La dirección no puede exceder los 255 caracteres.")]
        public string Direccion { get; set; }
    }
}
