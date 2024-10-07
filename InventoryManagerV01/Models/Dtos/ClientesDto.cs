using System.ComponentModel.DataAnnotations;

namespace InventoryManagerV01.Models.Dtos
{
    public class ClientesDto
    {
        public int ClienteID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [StringLength(255, ErrorMessage = "La dirección no puede exceder los 255 caracteres.")]
        public string Direccion { get; set; }
    }
}
