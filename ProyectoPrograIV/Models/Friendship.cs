using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograIV.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } 
        public Users User { get; set; }

        [Required]
        public string FriendId { get; set; } 
        public Users Friend { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}
