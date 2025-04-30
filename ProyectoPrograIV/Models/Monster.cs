using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograIV.Models
{
    public class Monster
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string? MonsterName { get; set; }
        [Display(Name = "Tipo Primario")]
        public string? MonsterType1 { get; set; }
        [Display(Name = "Tipo Secundario")]
        public string? MonsterType2 { get; set; }
        [Display(Name = "Ataque")]
        public int MonsterAttack { get; set; }
        [Display(Name = "Ataque Especial")]
        public int MonsterSpecialAttack { get; set; }
        [Display(Name = "Defensa")]
        public int MonsterDefense { get; set; }
        [Display(Name = "Defensa Especial")]
        public int MonsterSpecialDefense { get; set; }
        [Display(Name = "Velocidad")]
        public int MonsterSpeed { get; set; }
        [Display(Name = "Vida")]
        public int MonsterHealth { get;set; }
        [Display(Name = "Vida Actual")]
        public int MonsterCurrentHealth { get; set; }
        public string? Sprite { get; set; }

        public ICollection<Move> Moves { get; set; } = new List<Move>();
    }
}
