using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagerV01.Models.Dtos
{
    public class ActualizarProductoDto
    {
        [Required]
        public int ProductoID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
        public int CategoriaID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un proveedor válido.")]
        public int ProveedorID { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de compra debe ser mayor que 0.")]
        public decimal PrecioCompra { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor que 0.")]
        public decimal PrecioVenta { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo.")]
        public int StockMinimo { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Imagen { get; set; }

        public string? RutaIMagen { get; set; }
    }
}