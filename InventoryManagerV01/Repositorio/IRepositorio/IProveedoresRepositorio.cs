namespace InventoryManagerV01.Repositorio.IRepositorio
{
    public interface IProveedoresRepositorio
    {
        ICollection<Proveedores> GetProveedores();
        Proveedores GetProveedor(int proveedorId);
        bool ExisteProveedor(int id);
        bool ExisteProveedor(string nombre);
        bool CrearProveedor(Proveedores proveedor);
        bool ActualizarProveedor(Proveedores proveedor);
        bool BorrarProveedor(Proveedores proveedor);
        bool Guardar();
    }
}