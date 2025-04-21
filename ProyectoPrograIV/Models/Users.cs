using Microsoft.AspNetCore.Identity;

namespace ProyectoPrograIV.Models
{
    public class Users : IdentityUser
    {
        public string UserName { get; set; }

    }
}
