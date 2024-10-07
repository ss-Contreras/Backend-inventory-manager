using InventoryManagerV01.Data;
using InventoryManagerV01.Models;
using InventoryManagerV01.Repositorio.IRepositorio;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace InventoryManagerV01.Repositorio
{
    public class ProductosRepositorio : IProductosRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public ProductosRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarProducto(Productos producto)
        {
            try
            {
                var productoExistente = _bd.Productos.Find(producto.ProductoID);
                if (productoExistente == null)
                {
                    throw new KeyNotFoundException("No se encontró el producto para actualizar.");
                }

                _bd.Entry(productoExistente).CurrentValues.SetValues(producto);
                return Guardar();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool BorrarProducto(Productos producto)
        {
            try
            {
                if (!_bd.Productos.Any(p => p.ProductoID == producto.ProductoID))
                {
                    throw new KeyNotFoundException("No se encontró el producto para eliminar.");
                }

                _bd.Productos.Remove(producto);
                return Guardar();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CrearProducto(Productos producto)
        {
            try
            {
                if (producto.PrecioVenta < producto.PrecioCompra)
                {
                    throw new ArgumentException("El precio de venta no puede ser menor que el precio de compra.");
                }

                if (!_bd.Categorias.Any(c => c.CategoriaID == producto.CategoriaID))
                {
                    throw new KeyNotFoundException("La categoría especificada no existe.");
                }

                if (!_bd.Proveedores.Any(p => p.ProveedorID == producto.ProveedorID))
                {
                    throw new KeyNotFoundException("El proveedor especificado no existe.");
                }

                producto.FechaIngreso = DateTime.Now;
                _bd.Productos.Add(producto);
                return Guardar();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExisteProducto(int productoID)
        {
            try
            {
                return _bd.Productos.Any(c => c.ProductoID == productoID);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExisteProducto(string nombre)
        {
            try
            {
                return _bd.Productos.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Productos GetProducto(int productoID)
        {
            try
            {
                var producto = _bd.Productos
                    .Include(c => c.Categoria)
                    .Include(p => p.Proveedor)
                    .FirstOrDefault(c => c.ProductoID == productoID);

                if (producto == null)
                {
                    throw new KeyNotFoundException("No se encontró el producto.");
                }

                return producto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ICollection<Productos> GetProductos()
        {
            try
            {
                return _bd.Productos
                    .Include(c => c.Categoria)
                    .Include(p => p.Proveedor)
                    .OrderBy(c => c.Nombre)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Productos>();
            }
        }

        public IEnumerable<Productos> BuscarProductos(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    throw new ArgumentException("El criterio de búsqueda no puede estar vacío.");
                }

                return _bd.Productos
                    .Include(c => c.Categoria)
                    .Include(p => p.Proveedor)
                    .Where(p => p.Nombre.Contains(nombre))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Productos>();
            }
        }

        public ICollection<Productos> GetProductosEnCategoria(int categoriaID)
        {
            try
            {
                if (!_bd.Categorias.Any(c => c.CategoriaID == categoriaID))
                {
                    throw new KeyNotFoundException("La categoría especificada no existe.");
                }

                return _bd.Productos
                    .Include(c => c.Categoria)
                    .Include(p => p.Proveedor)
                    .Where(p => p.CategoriaID == categoriaID)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Productos>();
            }
        }

        public bool Guardar()
        {
            try
            {
                return _bd.SaveChanges() >= 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
