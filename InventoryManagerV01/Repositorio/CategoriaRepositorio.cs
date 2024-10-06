using InventoryManagerV01.Data;
using InventoryManagerV01.Models;
using InventoryManagerV01.Repositorio.IRepositorio;
using System.Linq;

namespace InventoryManagerV01.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public CategoriaRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCategoria(Categoria categoria)
        {
            //Arreglar problema del PUT
            var categoriaExistente = _bd.Categorias.Find(categoria.CategoriaID);
            if (categoriaExistente != null)
            {
                _bd.Entry(categoriaExistente).CurrentValues.SetValues(categoria);
            }
            else
            {
                _bd.Categorias.Update(categoria);
            }

            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoria)
        {
            _bd.Categorias.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            _bd.Categorias.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(int categoriaID)
        {
            return _bd.Categorias.Any(c => c.CategoriaID == categoriaID);
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _bd.Categorias.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public Categoria GetCategoria(int categoriaID)
        {
            return _bd.Categorias.FirstOrDefault(c => c.CategoriaID == categoriaID);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _bd.Categorias.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}