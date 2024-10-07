using InventoryManagerV01.Models;
using System.Collections.Generic;

namespace InventoryManagerV01.Repositorio.IRepositorio
{
    public interface IProductosRepositorio
    {
        ICollection<Productos> GetProductos();
        Productos GetProducto(int productoID);
        bool ExisteProducto(int productoID);
        bool ExisteProducto(string nombre);
        bool CrearProducto(Productos producto);
        bool ActualizarProducto(Productos producto);
        bool BorrarProducto(Productos producto);
        bool Guardar();
        IEnumerable<Productos> BuscarProductos(string nombre);
        ICollection<Productos> GetProductosEnCategoria(int categoriaID);
    }
}
