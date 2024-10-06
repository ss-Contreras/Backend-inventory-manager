using System.ComponentModel.DataAnnotations;

namespace InventoryManagerV01.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorID { get; set; }
        public string Nombre { get; set; }
        public int telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
    }
}
