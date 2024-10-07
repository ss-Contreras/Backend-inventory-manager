using InventoryManagerV01.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

public class EmpleadosRepositorio : IEmpleadosRepositorio
{
    private readonly ApplicationDbContext _bd;

    public EmpleadosRepositorio(ApplicationDbContext bd)
    {
        _bd = bd;
    }

    public ICollection<Empleados> GetEmpleados()
    {
        try
        {
            return _bd.Empleados.AsNoTracking().OrderBy(e => e.Nombre).ToList();
        }
        catch (Exception ex)
        {
            // Loguear el error si es necesario
            return new List<Empleados>();
        }
    }

    public Empleados GetEmpleado(int empleadoID)
    {
        try
        {
            var empleado = _bd.Empleados.AsNoTracking().FirstOrDefault(e => e.EmpleadoID == empleadoID);
            if (empleado == null)
            {
                throw new KeyNotFoundException("El empleado especificado no existe.");
            }
            return empleado;
        }
        catch (Exception ex)
        {
            // Loguear el error si es necesario
            return null;
        }
    }

    public bool ExisteEmpleado(int empleadoID)
    {
        try
        {
            return _bd.Empleados.Any(e => e.EmpleadoID == empleadoID);
        }
        catch (Exception ex)
        {
            // Loguear el error si es necesario
            return false;
        }
    }

    public bool ExisteEmpleado(string email)
    {
        try
        {
            return _bd.Empleados.Any(e => e.Email.ToLower().Trim() == email.ToLower().Trim());
        }
        catch (Exception ex)
        {
            // Loguear el error si es necesario
            return false;
        }
    }

    public bool CrearEmpleado(Empleados empleado)
    {
        try
        {
            if (ExisteEmpleado(empleado.Email))
            {
                throw new InvalidOperationException("Ya existe un empleado con el mismo email.");
            }

            _bd.Empleados.Add(empleado);
            return Guardar();
        }
        catch (Exception ex)
        {
            // Loguear el error si es necesario
            return false;
        }
    }

    public bool ActualizarEmpleado(Empleados empleado)
    {
        try
        {
            if (!ExisteEmpleado(empleado.EmpleadoID))
            {
                throw new KeyNotFoundException("El empleado especificado no existe.");
            }

            var empleadoExistente = _bd.Empleados.FirstOrDefault(e => e.EmpleadoID == empleado.EmpleadoID);
            if (empleadoExistente != null)
            {
                _bd.Entry(empleadoExistente).CurrentValues.SetValues(empleado);
                return Guardar();
            }
            return false;
        }
        catch (Exception ex)
        {
            // Loguear el error si es necesario
            return false;
        }
    }

    public bool BorrarEmpleado(Empleados empleado)
    {
        try
        {
            if (!ExisteEmpleado(empleado.EmpleadoID))
            {
                throw new KeyNotFoundException("El empleado especificado no existe.");
            }

            _bd.Empleados.Remove(empleado);
            return Guardar();
        }
        catch (Exception ex)
        {
            // Loguear el error si es necesario
            return false;
        }
    }

    public bool Guardar()
    {
        try
        {
            return _bd.SaveChanges() > 0;
        }
        catch (DbUpdateException ex)
        {
            // Loguear el error de la base de datos
            return false;
        }
        catch (Exception ex)
        {
            // Loguear cualquier otro error si es necesario
            return false;
        }
    }
}
