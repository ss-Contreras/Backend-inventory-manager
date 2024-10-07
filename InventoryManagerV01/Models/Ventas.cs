using InventoryManagerV01.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class Ventas
{
    [Key]
    public int VentaID { get; set; }

    [Required]
    public int ClienteID { get; set; }
    public Clientes Cliente { get; set; }

    [Required]
    public int EmpleadoID { get; set; }
    public Empleados Empleado { get; set; }

    [Required]
    public DateTime FechaVenta { get; set; }

    [Required]
    public decimal MontoTotal { get; set; }
}
