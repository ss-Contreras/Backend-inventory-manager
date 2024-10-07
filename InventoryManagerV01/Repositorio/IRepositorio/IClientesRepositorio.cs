using InventoryManagerV01.Models;
using System.Collections.Generic;

namespace InventoryManagerV01.Repositorio.IRepositorio
{
    public interface IClientesRepositorio
    {
        ICollection<Clientes> GetClientes();
        Clientes GetCliente(int clienteId);
        bool ExisteCliente(int id);
        bool ExisteCliente(string email);
        bool CrearCliente(Clientes cliente);
        bool ActualizarCliente(Clientes cliente);
        bool BorrarCliente(Clientes cliente);
        bool Guardar();
    }
}
