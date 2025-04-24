using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograIV.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public List<Monster> Monsters { get; set; }
    }
}
