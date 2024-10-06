using System.ComponentModel.DataAnnotations;

namespace InventoryManagerV01.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
