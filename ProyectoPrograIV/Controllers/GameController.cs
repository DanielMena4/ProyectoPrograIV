using Microsoft.AspNetCore.Mvc;
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

        return View(selectedTeam);
    }

}
