using System;

public class VentasDto
{
    public int VentaID { get; set; }
    public int ClienteID { get; set; }
    public string NombreCliente { get; set; }
    public int EmpleadoID { get; set; }
    public string NombreEmpleado { get; set; }
    public DateTime FechaVenta { get; set; }
    public decimal MontoTotal { get; set; }
}
