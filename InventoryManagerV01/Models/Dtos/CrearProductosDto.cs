using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace InventoryManagerV01.Models.Dtos
{
    public class CrearProductosDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        public int CategoriaID { get; set; }

        [Required]
        public int ProveedorID { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio de compra debe ser mayor o igual a cero.")]
        public decimal PrecioCompra { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor o igual a cero.")]
        public decimal PrecioVenta { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a cero.")]
        public int Stock { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo debe ser mayor o igual a cero.")]
        public int StockMinimo { get; set; }

        public IFormFile Imagen { get; set; }
    }
}
