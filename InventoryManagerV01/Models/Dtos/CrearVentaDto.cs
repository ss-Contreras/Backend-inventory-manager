using System;
using System.ComponentModel.DataAnnotations;

public class CrearVentaDto
{
    [Required]
    public int ClienteID { get; set; }

    [Required]
    public int EmpleadoID { get; set; }

    [Required]
    public DateTime FechaVenta { get; set; }

    [Required]
    public decimal MontoTotal { get; set; }
}
