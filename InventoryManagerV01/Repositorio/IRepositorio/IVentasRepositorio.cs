using System.Collections.Generic;

public interface IVentasRepositorio
{
    ICollection<Ventas> GetVentas();
    Ventas GetVenta(int ventaID);
    ICollection<Ventas> GetVentasPorCliente(int clienteID);
    ICollection<Ventas> GetVentasEntreFechas(DateTime fechaInicio, DateTime fechaFin);
    bool ExisteVenta(int ventaID);
    bool CrearVenta(Ventas venta);
    bool ActualizarVenta(Ventas venta);
    bool BorrarVenta(Ventas venta);
    bool Guardar();
}
