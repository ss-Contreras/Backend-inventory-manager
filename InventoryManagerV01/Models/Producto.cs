using System.ComponentModel.DataAnnotations;

namespace InventoryManagerV01.Models
{
    public class Producto
    {
        [Key]
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public int ProveedorID { get; set; }
        public Proveedor Proveedor { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
