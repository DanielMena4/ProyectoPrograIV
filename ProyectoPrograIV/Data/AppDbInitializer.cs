using ProyectoPrograIV.Models;

namespace ProyectoPrograIV.Data
{
    public class AppDbInitializer
    {
        public static void Seed(AppDBContext context)
        {
            if (!context.Monsters.Any())
            {
                var monsters = new List<Monster>
                {
                    new Monster { MonsterName = "Amaru", Sprite = "../imagenes/Monstruos/Amaru/Amaru_1.png", MonsterAttack = 50, MonsterDefense = 40, MonsterSpecialAttack = 30, MonsterSpecialDefense = 25, MonsterSpeed = 20, MonsterHealth = 100, MonsterCurrentHealth = 100, MonsterType1 = "Dragon", MonsterType2 = null },
                    new Monster { MonsterName = "Capibara", Sprite = "../imagenes/Monstruos/Capibara/Capibara_1.png", MonsterAttack = 30, MonsterDefense = 50, MonsterSpecialAttack = 25, MonsterSpecialDefense = 40, MonsterSpeed = 25, MonsterHealth = 110, MonsterCurrentHealth = 110, MonsterType1 = "Agua", MonsterType2 = null },
                    new Monster { MonsterName = "Comburucho", Sprite = "../imagenes/Monstruos/Comburucho/Comburucho_1.png", MonsterAttack = 45, MonsterDefense = 35, MonsterSpecialAttack = 40, MonsterSpecialDefense = 30, MonsterSpeed = 30, MonsterHealth = 90, MonsterCurrentHealth = 90, MonsterType1 = "Fuego", MonsterType2 = "Fantasma" },
                    new Monster { MonsterName = "Don Gallon", Sprite = "../imagenes/Monstruos/DonGallon/DonGallon_1.png", MonsterAttack = 60, MonsterDefense = 60, MonsterSpecialAttack = 20, MonsterSpecialDefense = 20, MonsterSpeed = 15, MonsterHealth = 120, MonsterCurrentHealth = 120, MonsterType1 = "Lucha", MonsterType2 = "Volador" },
                    new Monster { MonsterName = "Venus", Sprite = "../imagenes/Monstruos/Venus/Venus_1.png", MonsterAttack = 35, MonsterDefense = 45, MonsterSpecialAttack = 50, MonsterSpecialDefense = 50, MonsterSpeed = 25, MonsterHealth = 95, MonsterCurrentHealth = 95, MonsterType1 = "Psiquico", MonsterType2 = "Roca" },
                    new Monster { MonsterName = "Volpaca", Sprite = "../imagenes/Monstruos/Volpaca/Volpaca_1.png", MonsterAttack = 40, MonsterDefense = 30, MonsterSpecialAttack = 60, MonsterSpecialDefense = 45, MonsterSpeed = 35, MonsterHealth = 85, MonsterCurrentHealth = 85, MonsterType1 = "Tierra", MonsterType2 = "Hielo" }
                };

                context.Monsters.AddRange(monsters);
                context.SaveChanges();
            }
        }
    }
}
