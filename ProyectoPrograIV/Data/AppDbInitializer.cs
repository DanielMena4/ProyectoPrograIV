using ProyectoPrograIV.Models;

namespace ProyectoPrograIV.Data
{
    public class AppDbInitializer
    {
        public static void Seed(AppDBContext context)
        {
            if (!context.Monsters.Any())
            {
                var movimientos = new List<Move>();
                var velocidadExtrema = new Move { Type = "Normal", Name = "Velocidad Extrema", Power = 80, Priority = 2, Class = "Physical" };
                var colaDragon = new Move { Type = "Dragon", Name = "Cola Dragon", Power = 110, Priority = 0, Class = "Physical" };
                var golpeAereo = new Move { Type = "Volador", Name = "Golpe Aéreo", Power = 110, Priority = 0, Class = "Physical" };
                var lanzallamas = new Move { Type = "Fuego", Name = "Lanzallamas", Power = 120, Priority = 0, Class = "Special" };
                var hidroariete = new Move { Type = "Agua", Name = "Hidroariete", Power = 90, Priority = 0, Class = "Physical" };
                var ataqueRapido = new Move { Type = "Normal", Name = "Ataque Rápido", Power = 60, Priority = 1, Class = "Physical" };
                var surf = new Move { Type = "Agua", Name = "Surf", Power = 90, Priority = 0, Class = "Special" };
                var esquirlaHelada = new Move { Type = "Hielo", Name = "Esquirla Helada", Power = 90, Priority = 0, Class = "Physical" };
                var bolaSombra = new Move { Type = "Fantasma", Name = "Bola Sombra", Power = 100, Priority = 0, Class = "Special" };
                var pulsoSombrio = new Move { Type = "Siniestro", Name = "Pulso sombrio", Power = 100, Priority = 0, Class = "Special" };
                var golpeBajo = new Move { Type = "Siniestro", Name = "Golpe Bajo", Power = 60, Priority = 1, Class = "Physical" };
                var patadaAlta = new Move { Type = "Lucha", Name = "Patada Alta", Power = 100, Priority = 0, Class = "Physical" };
                var golpeTrueno = new Move { Type = "Electrico", Name = "Golpe Trueno", Power = 100, Priority = 0, Class = "Physical" };
                var psiquico = new Move { Type = "Psiquico", Name = "Ataque Psíquico", Power = 100, Priority = 0, Class = "Special" };
                var poderAntiguo = new Move { Type = "Roca", Name = "Poder Antiguo", Power = 100, Priority = 0, Class = "Special" };
                var pedrada = new Move { Type = "Roca", Name = "Pedrada", Power = 100, Priority = 0, Class = "Physical" };
                var tierraViva = new Move { Type = "Tierra", Name = "Tierra Viva", Power = 100, Priority = 0, Class = "Special" };
                var terremoto = new Move { Type = "Tierra", Name = "Terremoto", Power = 110, Priority = 0, Class = "Physical" };

                var allMoves = new List<Move>
                {
                    velocidadExtrema, colaDragon, golpeAereo, lanzallamas, hidroariete,
                    ataqueRapido, surf, esquirlaHelada, bolaSombra, pulsoSombrio,
                    golpeBajo, patadaAlta, golpeTrueno, psiquico, poderAntiguo,
                    pedrada, tierraViva, terremoto
                };
                context.Moves.AddRange(allMoves);
                context.SaveChanges();

                var monsters = new List<Monster>();
                var amaru = new Monster { MonsterName = "Amaru", Sprite = "../imagenes/Monstruos/Amaru/Amaru_1.png", MonsterAttack = 50, MonsterDefense = 40, MonsterSpecialAttack = 30, MonsterSpecialDefense = 25, MonsterSpeed = 20, MonsterHealth = 100, MonsterCurrentHealth = 100, MonsterType1 = "Dragon", MonsterType2 = null };
                amaru.Moves.Add(velocidadExtrema);
                amaru.Moves.Add(colaDragon);
                amaru.Moves.Add(golpeAereo);
                amaru.Moves.Add(lanzallamas);
                monsters.Add(amaru);
                
                var capibara = new Monster { MonsterName = "Capibara", Sprite = "../imagenes/Monstruos/Capibara/Capibara_1.png", MonsterAttack = 30, MonsterDefense = 50, MonsterSpecialAttack = 25, MonsterSpecialDefense = 40, MonsterSpeed = 25, MonsterHealth = 110, MonsterCurrentHealth = 110, MonsterType1 = "Agua", MonsterType2 = null };
                capibara.Moves.Add(hidroariete);
                capibara.Moves.Add(surf);
                capibara.Moves.Add(ataqueRapido);
                capibara.Moves.Add(esquirlaHelada);
                monsters.Add(capibara);

                var comburucho = new Monster { MonsterName = "Comburucho", Sprite = "../imagenes/Monstruos/Comburucho/Comburucho_1.png", MonsterAttack = 45, MonsterDefense = 35, MonsterSpecialAttack = 40, MonsterSpecialDefense = 30, MonsterSpeed = 30, MonsterHealth = 90, MonsterCurrentHealth = 90, MonsterType1 = "Fuego", MonsterType2 = "Fantasma" };
                comburucho.Moves.Add(bolaSombra);
                comburucho.Moves.Add(pulsoSombrio);
                comburucho.Moves.Add(golpeBajo);
                comburucho.Moves.Add(lanzallamas);
                monsters.Add(comburucho);

                var donGallon = new Monster { MonsterName = "Don Gallon", Sprite = "../imagenes/Monstruos/DonGallon/DonGallon_1.png", MonsterAttack = 60, MonsterDefense = 60, MonsterSpecialAttack = 20, MonsterSpecialDefense = 20, MonsterSpeed = 15, MonsterHealth = 120, MonsterCurrentHealth = 120, MonsterType1 = "Lucha", MonsterType2 = "Volador" };
                donGallon.Moves.Add(patadaAlta);
                donGallon.Moves.Add(golpeAereo);
                donGallon.Moves.Add(golpeTrueno);
                donGallon.Moves.Add(ataqueRapido);
                monsters.Add(donGallon);

                var venus = new Monster { MonsterName = "Venus", Sprite = "../imagenes/Monstruos/Venus/Venus_1.png", MonsterAttack = 35, MonsterDefense = 45, MonsterSpecialAttack = 50, MonsterSpecialDefense = 50, MonsterSpeed = 25, MonsterHealth = 95, MonsterCurrentHealth = 95, MonsterType1 = "Psiquico", MonsterType2 = "Roca" };
                venus.Moves.Add(psiquico);
                venus.Moves.Add(poderAntiguo);
                venus.Moves.Add(pedrada);
                venus.Moves.Add(tierraViva);
                monsters.Add(venus);

                var volpaca = new Monster { MonsterName = "Volpaca", Sprite = "../imagenes/Monstruos/Volpaca/Volpaca_1.png", MonsterAttack = 40, MonsterDefense = 30, MonsterSpecialAttack = 60, MonsterSpecialDefense = 45, MonsterSpeed = 35, MonsterHealth = 85, MonsterCurrentHealth = 85, MonsterType1 = "Tierra", MonsterType2 = "Hielo" };
                volpaca.Moves.Add(esquirlaHelada);
                volpaca.Moves.Add(lanzallamas);
                volpaca.Moves.Add(tierraViva);
                volpaca.Moves.Add(terremoto);
                monsters.Add(volpaca);

                context.Monsters.AddRange(monsters);
                context.SaveChanges();
            }
        }
    }
}
