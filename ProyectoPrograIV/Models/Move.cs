using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoPrograIV.Models
{
    public class Move
    {
        public int Id { get; set; }
        [Display(Name = "Tipo")]
        public string? Type { get; set; }
        [Display(Name = "Nombre")]
        public string? Name { get; set; }
        [Display(Name = "Poder")]
        public int Power { get; set; }
        [Display(Name = "Prioridad")]
        public int Priority { get; set; }
        [Display(Name = "Clase")]
        public string? Class { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Monster> Monsters { get; set; } = new List<Monster>();
    }
}
