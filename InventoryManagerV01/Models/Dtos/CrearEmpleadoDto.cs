using System;
using System.ComponentModel.DataAnnotations;

public class CrearEmpleadoDto
{
    [Required(ErrorMessage = "El nombre del empleado es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public string Email { get; set; }

    [MaxLength(255, ErrorMessage = "La dirección no puede tener más de 255 caracteres")]
    public string Direccion { get; set; }

    [Required(ErrorMessage = "El cargo es obligatorio")]
    [MaxLength(50, ErrorMessage = "El cargo no puede tener más de 50 caracteres")]
    public string Cargo { get; set; }

    [Required(ErrorMessage = "La fecha de contratación es obligatoria")]
    public DateTime FechaContratacion { get; set; }
}
