using InventoryManagerV01.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

public class VentasRepositorio : IVentasRepositorio
{
    private readonly ApplicationDbContext _bd;

    public VentasRepositorio(ApplicationDbContext bd)
    {
        _bd = bd;
    }

    public ICollection<Ventas> GetVentas()
    {
        try
        {
            return _bd.Ventas
                .Include(c => c.Cliente)
                .Include(e => e.Empleado)
                .OrderBy(v => v.FechaVenta)
                .ToList();
        }
        catch (Exception ex)
        {
            // Log the exception here
            return new List<Ventas>();
        }
    }

    public Ventas GetVenta(int ventaID)
    {
        try
        {
            var venta = _bd.Ventas
                .Include(c => c.Cliente)
                .Include(e => e.Empleado)
                .FirstOrDefault(v => v.VentaID == ventaID);

            if (venta == null)
            {
                throw new KeyNotFoundException("No se encontró la venta especificada.");
            }

            return venta;
        }
        catch (Exception ex)
        {
            // Log the exception here
            return null;
        }
    }

    public ICollection<Ventas> GetVentasPorCliente(int clienteID)
    {
        try
        {
            if (!_bd.Clientes.Any(c => c.ClienteID == clienteID))
            {
                throw new KeyNotFoundException("No se encontró el cliente especificado.");
            }

            return _bd.Ventas
                .Include(c => c.Cliente)
                .Include(e => e.Empleado)
                .Where(v => v.ClienteID == clienteID)
                .ToList();
        }
        catch (Exception ex)
        {
            // Log the exception here
            return new List<Ventas>();
        }
    }

    public ICollection<Ventas> GetVentasEntreFechas(DateTime fechaInicio, DateTime fechaFin)
    {
        try
        {
            if (fechaInicio > fechaFin)
            {
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin.");
            }

            return _bd.Ventas
                .Include(c => c.Cliente)
                .Include(e => e.Empleado)
                .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                .ToList();
        }
        catch (Exception ex)
        {
            // Log the exception here
            return new List<Ventas>();
        }
    }

    public bool ExisteVenta(int ventaID)
    {
        try
        {
            return _bd.Ventas.Any(v => v.VentaID == ventaID);
        }
        catch (Exception ex)
        {
            // Log the exception here
            return false;
        }
    }

    public bool CrearVenta(Ventas venta)
    {
        try
        {
            if (!_bd.Clientes.Any(c => c.ClienteID == venta.ClienteID))
            {
                throw new KeyNotFoundException("El cliente especificado no existe.");
            }

            if (!_bd.Empleados.Any(e => e.EmpleadoID == venta.EmpleadoID))
            {
                throw new KeyNotFoundException("El empleado especificado no existe.");
            }

            _bd.Ventas.Add(venta);
            return Guardar();
        }
        catch (Exception ex)
        {
            // Log the exception here
            return false;
        }
    }

    public bool ActualizarVenta(Ventas venta)
    {
        try
        {
            if (!_bd.Ventas.Any(v => v.VentaID == venta.VentaID))
            {
                throw new KeyNotFoundException("La venta especificada no existe.");
            }

            if (!_bd.Clientes.Any(c => c.ClienteID == venta.ClienteID))
            {
                throw new KeyNotFoundException("El cliente especificado no existe.");
            }

            if (!_bd.Empleados.Any(e => e.EmpleadoID == venta.EmpleadoID))
            {
                throw new KeyNotFoundException("El empleado especificado no existe.");
            }

            _bd.Ventas.Update(venta);
            return Guardar();
        }
        catch (Exception ex)
        {
            // Log the exception here
            return false;
        }
    }

    public bool BorrarVenta(Ventas venta)
    {
        try
        {
            if (!_bd.Ventas.Any(v => v.VentaID == venta.VentaID))
            {
                throw new KeyNotFoundException("La venta especificada no existe.");
            }

            _bd.Ventas.Remove(venta);
            return Guardar();
        }
        catch (Exception ex)
        {
            // Log the exception here
            return false;
        }
    }

    public bool Guardar()
    {
        try
        {
            return _bd.SaveChanges() >= 0;
        }
        catch (DbUpdateException ex)
        {
            // Log the exception here
            return false;
        }
        catch (Exception ex)
        {
            // Log the exception here
            return false;
        }
    }
}
