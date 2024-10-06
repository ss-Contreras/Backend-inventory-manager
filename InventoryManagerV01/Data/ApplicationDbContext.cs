using InventoryManagerV01.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerV01.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUsuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<AppUsuario> AppUsuario { get; set; }


    }
}
