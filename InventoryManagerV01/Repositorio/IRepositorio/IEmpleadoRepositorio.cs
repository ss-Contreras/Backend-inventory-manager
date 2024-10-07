using System.Collections.Generic;

public interface IEmpleadosRepositorio
{
    ICollection<Empleados> GetEmpleados();
    Empleados GetEmpleado(int empleadoID);
    bool ExisteEmpleado(int empleadoID);
    bool ExisteEmpleado(string email);
    bool CrearEmpleado(Empleados empleado);
    bool ActualizarEmpleado(Empleados empleado);
    bool BorrarEmpleado(Empleados empleado);
    bool Guardar();
}
