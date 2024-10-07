using InventoryManagerV01.Data;
using InventoryManagerV01.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerV01.Repositorio
{
    public class ProveedoresRepositorio : IProveedoresRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public ProveedoresRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarProveedor(Proveedores proveedor)
        {
            var proveedorExistente = _bd.Proveedores.AsNoTracking().FirstOrDefault(p => p.ProveedorID == proveedor.ProveedorID);
            if (proveedorExistente == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(proveedor.Nombre) || proveedor.Nombre.Length > 100)
            {
                throw new ArgumentException("Nombre del proveedor es inválido.");
            }

            if (proveedor.Telefono.Length > 15 || !IsValidPhone(proveedor.Telefono))
            {
                throw new ArgumentException("El formato del número de teléfono no es válido.");
            }

            if (!IsValidEmail(proveedor.Email))
            {
                throw new ArgumentException("El formato del correo electrónico no es válido.");
            }

            _bd.Proveedores.Update(proveedor);
            return Guardar();
        }

        public bool BorrarProveedor(Proveedores proveedor)
        {
            if (!_bd.Proveedores.Any(p => p.ProveedorID == proveedor.ProveedorID))
            {
                return false;
            }
            _bd.Proveedores.Remove(proveedor);
            return Guardar();
        }

        public bool CrearProveedor(Proveedores proveedor)
        {
            if (string.IsNullOrEmpty(proveedor.Nombre) || proveedor.Nombre.Length > 100)
            {
                throw new ArgumentException("Nombre del proveedor es inválido.");
            }

            if (proveedor.Telefono.Length > 15 || !IsValidPhone(proveedor.Telefono))
            {
                throw new ArgumentException("El formato del número de teléfono no es válido.");
            }

            if (!IsValidEmail(proveedor.Email))
            {
                throw new ArgumentException("El formato del correo electrónico no es válido.");
            }

            _bd.Proveedores.Add(proveedor);
            return Guardar();
        }

        public bool ExisteProveedor(int proveedorID)
        {
            return _bd.Proveedores.Any(p => p.ProveedorID == proveedorID);
        }

        public bool ExisteProveedor(string nombre)
        {
            return _bd.Proveedores.Any(p => p.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }

        public Proveedores GetProveedor(int proveedorID)
        {
            return _bd.Proveedores.FirstOrDefault(p => p.ProveedorID == proveedorID);
        }

        public ICollection<Proveedores> GetProveedores()
        {
            return _bd.Proveedores.OrderBy(p => p.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0;
        }

        private bool IsValidPhone(string telefono)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\+?[0-9]*$");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}