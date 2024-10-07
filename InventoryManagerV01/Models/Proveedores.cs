using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Proveedores
{
    [Key]
    public int ProveedorID { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }

    // Relación con productos
    public ICollection<Productos> Productos { get; set; }
}
