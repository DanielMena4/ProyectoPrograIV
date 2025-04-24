using ProyectoPrograIV.Models;

namespace ProyectoPrograIV.ViewModels
{
    public class PlayViewModel
    {
        public string Monster1 { get; set; }
        public string Monster2 { get; set; }
        public string Monster3 { get; set; }
        public string Monster4 { get; set; }
        public string Monster5 { get; set; }
        public string Monster6 { get; set; }    
        public List<Monster> Team { get; set; }
        public PlayViewModel()
        {
            Team = new List<Monster>
            {
                new Monster { MonsterName = "Amaru", Sprite = "../imagenes/Monstruos/Amaru/Amaru_1.png" },
                new Monster { MonsterName = "Capibara", Sprite = "../imagenes/Monstruos/Capibara/Capibara_1.png" },
                new Monster { MonsterName = "Comburucho", Sprite = "../imagenes/Monstruos/Comburucho/Comburucho_1.png" },
                new Monster { MonsterName = "Don Gallon", Sprite = "../imagenes/Monstruos/DonGallon/DonGallon_1.png" },
                new Monster { MonsterName = "Venus", Sprite = "../imagenes/Monstruos/Venus/Venus_1.png" },
                new Monster { MonsterName = "Volpaca", Sprite = "../imagenes/Monstruos/Volpaca/Volpaca_1.png" }
            };
        }

    }
}
