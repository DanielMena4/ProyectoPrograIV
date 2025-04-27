using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProyectoPrograIV.Data;
using ProyectoPrograIV.Extensions;
using ProyectoPrograIV.Models;
using ProyectoPrograIV.ViewModels;

public class GameController : Controller
{
    private readonly AppDBContext _context;

    public GameController(AppDBContext context)
    {
        _context = context;
    }

    private static readonly Dictionary<string, Dictionary<string, double>> typeInteractions = new Dictionary<string, Dictionary<string, double>>
    {
        {
            "Hielo", new Dictionary<string, double>
            {
                { "Lucha", 2.0 },
                { "Roca", 2.0 },
                { "Fuego", 2.0 },
                { "Hielo", 0.5 }
            }
        },
        {
            "Tierra", new Dictionary<string, double>
            {
                { "Electrico", 0.0 },
                { "Roca", 0.5 },
                { "Hielo", 2.0 },
                { "Agua", 2.0 }
            }
        },
        {
            "Lucha", new Dictionary<string, double>
            {
                { "Fantasma", 2.0 },
                { "Psiquico", 2.0 }
            }
        },
        {
            "Psiquico", new Dictionary<string, double>
            {
                { "Fantasma", 2.0 },
                { "Siniestro", 2.0 },
                { "Lucha", 0.5 },
                { "Psiquico", 0.5 }
            }
        },
        {
            "Dragon", new Dictionary<string, double>
            {
                { "Dragon", 2.0 },
                { "Hielo", 2.0 },
                { "Agua", 0.5 },
                { "Fuego", 0.5 },
                { "Electrico", 0.5 }
            }
        },
        {
            "Fantasma", new Dictionary<string, double>
            {
                { "Fantasma", 2.0 },
                { "Siniestro", 2.0 },
                { "Lucha", 0.0 },
                { "Normal", 0.0 },
                { "Psiquico", 0.5 }
            }
        },
        {
            "Fuego", new Dictionary<string, double>
            {
                { "Agua", 2.0 },
                { "Tierra", 2.0 },
                { "Roca", 2.0 },
                { "Fuego", 0.5 },
                { "Hielo", 0.5 }
            }
        },
        {
            "Roca", new Dictionary<string, double>
            {
                { "Normal", 0.5 },
                { "Fuego", 0.5 },
                { "Lucha", 2.0 },
                { "Tierra", 2.0 },
                { "Agua", 2.0 }
            }
        },
        {
            "Volador", new Dictionary<string, double>
            {
                { "Lucha", 0.5 },
                { "Tierra", 0.0 },
                { "Roca", 2.0 },
                { "Electrico", 2.0 },
                { "Hielo", 2.0 }
            }
        }
    };

    public IActionResult Play()
    {
        var monsters = _context.Monsters.ToList();
        var viewModel = new PlayViewModel
        {
            Team = monsters
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Play(PlayViewModel viewModel)
    {
        var selectedMonsters = viewModel.MonsterNames;
        var selectedTeam = new Team
        {
            Monsters = new List<Monster>()
        };

        foreach (var monsterName in selectedMonsters)
        {
            if (string.IsNullOrWhiteSpace(monsterName)) continue;
            var cleanedMonsterName = monsterName.Trim().ToLower();
            var monster = _context.Monsters
                .Include(m => m.Moves)
                .FirstOrDefault(m => m.MonsterName.ToLower().Trim() == cleanedMonsterName);

            if (monster != null)
            {
                selectedTeam.Monsters.Add(monster);
            }
            else
            {
                Console.WriteLine($"No se encontró el monstruo: '{monsterName}'");
            }
        }

        if (selectedTeam.Monsters.Count > 0)
        {
            HttpContext.Session.SetObjectAsJson("SelectedTeam", selectedTeam);
        }
        else
        {
            Console.WriteLine("No se seleccionaron monstruos.");
        }

        return RedirectToAction("Battle");
    }

    public IActionResult Battle()
    {
        var selectedTeam = HttpContext.Session.GetObjectFromJson<Team>("SelectedTeam");

        if (selectedTeam == null || selectedTeam.Monsters == null || !selectedTeam.Monsters.Any())
        {
            return RedirectToAction("Play");
        }

        var enemyTeam = HttpContext.Session.GetObjectFromJson<Team>("EnemyTeam");
        if (enemyTeam == null || enemyTeam.Monsters == null || !enemyTeam.Monsters.Any())
        {
            var allMonsters = _context.Monsters
                .Include(m => m.Moves)
                .AsEnumerable()
                .Select(m =>
                {
                    m.Moves = m.Moves.ToList();
                    return m;
                })
                .ToList();
            var random = new Random();
            var randomMonsters = allMonsters.OrderBy(x => random.Next()).Take(6).ToList();

            foreach (var monster in randomMonsters)
            {
                monster.MonsterCurrentHealth = monster.MonsterHealth;
            }

            enemyTeam = new Team { Monsters = randomMonsters };
            HttpContext.Session.SetObjectAsJson("EnemyTeam", enemyTeam);
        }

        if (HttpContext.Session.GetInt32("ActiveInstanceId") == null)
            HttpContext.Session.SetInt32("ActiveInstanceId", 0);
        if (HttpContext.Session.GetInt32("EnemyActiveIndex") == null)
            HttpContext.Session.SetInt32("EnemyActiveIndex", 0);

        var activePlayerMonsters = selectedTeam.Monsters.Where(m => m.MonsterCurrentHealth > 0).ToList();
        var activeEnemyMonsters = enemyTeam.Monsters.Where(m => m.MonsterCurrentHealth > 0).ToList();

        ViewData["ActivePlayerMonsters"] = activePlayerMonsters;
        ViewData["ActiveEnemyMonsters"] = activeEnemyMonsters;

        ViewData["ActiveInstanceId"] = HttpContext.Session.GetInt32("ActiveInstanceId");
        ViewData["EnemyActiveIndex"] = HttpContext.Session.GetInt32("EnemyActiveIndex");

        var battleLog = HttpContext.Session.GetObjectFromJson<List<string>>("BattleLog") ?? new List<string>();
        ViewData["BattleLog"] = battleLog;

        return View((PlayerTeam: selectedTeam, EnemyTeam: enemyTeam));
    }

    public IActionResult ChangeMonster(string monsterName, int instanceId)
    {
        HttpContext.Session.SetInt32("ActiveInstanceId", instanceId);
        return RedirectToAction("Battle");
    }

    [HttpPost]
public IActionResult Attack(int moveId)
{
    var playerTeam = HttpContext.Session.GetObjectFromJson<Team>("SelectedTeam");
    var enemyTeam = HttpContext.Session.GetObjectFromJson<Team>("EnemyTeam");

    var playerIndex = HttpContext.Session.GetInt32("ActiveInstanceId") ?? 0;
    var enemyIndex = HttpContext.Session.GetInt32("EnemyActiveIndex") ?? 0;

    var playerMonster = playerTeam.Monsters[playerIndex];
    var enemyMonster = enemyTeam.Monsters[enemyIndex];

    if (playerMonster.MonsterCurrentHealth <= 0 || enemyMonster.MonsterCurrentHealth <= 0)
    {
        return RedirectToAction("Battle");
    }

    var move = playerMonster.Moves.FirstOrDefault(m => m.Id == moveId);
    if (move == null)
    {
        return RedirectToAction("Battle");
    }

    int CalculateDamage(Monster attacker, Monster defender, Move move)
    {
        int baseDamage = move.Power;
        int attackStat = move.Class == "Special" ? attacker.MonsterSpecialAttack : attacker.MonsterAttack;
        int defenseStat = move.Class == "Special" ? defender.MonsterSpecialDefense : defender.MonsterDefense;

        double typeEffectiveness = GetTypeEffectiveness(attacker, defender, move);

        int damage = (int)(baseDamage * ((double)attackStat / (defenseStat + 1)) * typeEffectiveness);

        if (attacker.MonsterType1 == move.Type || attacker.MonsterType2 == move.Type)
        {
            damage = (int)(damage * 1.5);
        }

        return Math.Min(damage, defender.MonsterCurrentHealth);
    }

        double GetTypeEffectiveness(Monster attacker, Monster defender, Move move)
        {
            var attackerTypes = new List<string> { attacker.MonsterType1, attacker.MonsterType2 };
            var defenderTypes = new List<string> { defender.MonsterType1, defender.MonsterType2 };

            double effectiveness = 1.0;

            foreach (var defenderType in defenderTypes)
            {
                foreach (var attackerType in attackerTypes)
                {
                    if (!string.IsNullOrEmpty(attackerType) && !string.IsNullOrEmpty(defenderType))
                    {
                        if (typeInteractions.ContainsKey(attackerType) && typeInteractions[attackerType].ContainsKey(defenderType))
                        {
                            effectiveness *= typeInteractions[attackerType][defenderType];
                        }
                        else
                        {
                            effectiveness *= 1.0;
                        }
                    }
                }
            }

            return effectiveness;
        }


        var playerMonstersOrdered = playerTeam.Monsters
        .Where(m => m.MonsterCurrentHealth > 0)
        .Select(m => new { Monster = m, Moves = m.Moves.OrderBy(mv => mv.Priority).ThenByDescending(mv => m.MonsterSpeed) })
        .ToList();

    var enemyMonstersOrdered = enemyTeam.Monsters
        .Where(m => m.MonsterCurrentHealth > 0)
        .Select(m => new { Monster = m, Moves = m.Moves.OrderBy(mv => mv.Priority).ThenByDescending(mv => m.MonsterSpeed) })
        .ToList();

    var currentPlayerMonster = playerMonstersOrdered.FirstOrDefault()?.Monster;
    var currentEnemyMonster = enemyMonstersOrdered.FirstOrDefault()?.Monster;

    if (currentPlayerMonster == null || currentEnemyMonster == null)
    {
        return RedirectToAction("Battle");
    }

    var battleLog = HttpContext.Session.GetObjectFromJson<List<string>>("BattleLog") ?? new List<string>();

    if (currentPlayerMonster.MonsterCurrentHealth > 0)
    {
        var damageToEnemy = CalculateDamage(currentPlayerMonster, currentEnemyMonster, move);
        currentEnemyMonster.MonsterCurrentHealth -= damageToEnemy;

        battleLog.Add($"El monstruo {currentPlayerMonster.MonsterName} usó {move.Name} contra {currentEnemyMonster.MonsterName}. Daño: {damageToEnemy}");

        if (currentEnemyMonster.MonsterCurrentHealth <= 0)
        {
            battleLog.Add($"{currentEnemyMonster.MonsterName} ha sido derrotado.");
            var nextEnemyIndex = enemyTeam.Monsters.FindIndex(m => m.MonsterCurrentHealth > 0);
            HttpContext.Session.SetInt32("EnemyActiveIndex", nextEnemyIndex);
        }
    }

    if (currentEnemyMonster.MonsterCurrentHealth > 0)
    {
        var enemyMove = currentEnemyMonster.Moves.OrderBy(m => Guid.NewGuid()).First();
        var damageToPlayer = CalculateDamage(currentEnemyMonster, currentPlayerMonster, enemyMove);
        currentPlayerMonster.MonsterCurrentHealth -= damageToPlayer;

        battleLog.Add($"El monstruo {currentEnemyMonster.MonsterName} usó {enemyMove.Name} contra {currentPlayerMonster.MonsterName}. Daño: {damageToPlayer}");

        if (currentPlayerMonster.MonsterCurrentHealth <= 0)
        {
            battleLog.Add($"{currentPlayerMonster.MonsterName} ha sido derrotado.");
            var nextPlayerIndex = playerTeam.Monsters.FindIndex(m => m.MonsterCurrentHealth > 0);
            HttpContext.Session.SetInt32("ActiveInstanceId", nextPlayerIndex);
        }
    }

    if (currentPlayerMonster.MonsterCurrentHealth <= 0)
    {
        var nextPlayerIndex = playerTeam.Monsters.FindIndex(m => m.MonsterCurrentHealth > 0);
        if (nextPlayerIndex != -1)
        {
            HttpContext.Session.SetInt32("ActiveInstanceId", nextPlayerIndex);
        }
        else
        {
            return RedirectToAction("EndBattle", new { playerWon = false });
        }
    }

    if (currentEnemyMonster.MonsterCurrentHealth <= 0)
    {
        var nextEnemyIndex = enemyTeam.Monsters.FindIndex(m => m.MonsterCurrentHealth > 0);
        if (nextEnemyIndex == -1)
        {
            return RedirectToAction("EndBattle", new { playerWon = true });
        }
        else
        {
            HttpContext.Session.SetInt32("EnemyActiveIndex", nextEnemyIndex);
        }
    }

    HttpContext.Session.SetObjectAsJson("BattleLog", battleLog);
    HttpContext.Session.SetObjectAsJson("SelectedTeam", playerTeam);
    HttpContext.Session.SetObjectAsJson("EnemyTeam", enemyTeam);

    return RedirectToAction("Battle");
}

    public IActionResult EndBattle(bool playerWon)
    {
        HttpContext.Session.Remove("SelectedTeam");
        HttpContext.Session.Remove("EnemyTeam");
        HttpContext.Session.Remove("ActiveInstanceId");
        HttpContext.Session.Remove("EnemyActiveIndex");
        ViewBag.PlayerWon = playerWon;

        return View();
    }
}
