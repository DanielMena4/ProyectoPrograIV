using Microsoft.AspNetCore.Identity;

namespace ProyectoPrograIV.Models
{
    public class Users : IdentityUser
    {
        public ICollection<Friendship> Friends { get; set; }
        public ICollection<Friendship> FriendOf { get; set; }

    }
}
