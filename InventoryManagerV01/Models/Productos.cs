using InventoryManagerV01.Models;
using System.ComponentModel.DataAnnotations;

public class Productos
{
    [Key]
    public int ProductoID { get; set; }
    public string Nombre { get; set; }

    public int CategoriaID { get; set; }
    public Categoria Categoria { get; set; }

    public int ProveedorID { get; set; }
    public Proveedores Proveedor { get; set; }  // Aquí está la referencia a la clase Proveedor

    public decimal PrecioCompra { get; set; }
    public decimal PrecioVenta { get; set; }
    public int Stock { get; set; }
    public int StockMinimo { get; set; }
    public DateTime FechaIngreso { get; set; }

    public string RutaIMagen { get; set; }
    public string RutaLocalIMagen { get; set; }
}
