namespace InventoryManagerV01.Models.Dtos
{
    public class ActualizarEmpleadoDto
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Cargo { get; set; }
        public DateTime FechaContratacion { get; set; }
    }
}