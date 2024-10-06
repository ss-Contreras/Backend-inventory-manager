using Microsoft.AspNetCore.Identity;

namespace InventoryManagerV01.Models
{
    public class AppUsuario : IdentityUser
    {
        public string Nombre { get; set; }
    }

}
