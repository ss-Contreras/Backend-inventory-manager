using System;

namespace InventoryManagerV01.Models.Dtos
{
    public class ProductosDto
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public int CategoriaID { get; set; }
        public string CategoriaNombre { get; set; }
        public int ProveedorID { get; set; }
        public string ProveedorNombre { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string RutaIMagen { get; set; }
    }
}
