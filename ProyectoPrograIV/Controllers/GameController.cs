using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoPrograIV.Models;
using ProyectoPrograIV.ViewModels;

namespace ProyectoPrograIV.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Play()
        {
            var viewModel = new PlayViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Play(PlayViewModel viewModel)
        {
            var selectedMonsters = new List<string>
            {
                viewModel.Monster1,
                viewModel.Monster2,
                viewModel.Monster3,
                viewModel.Monster4,
                viewModel.Monster5,
                viewModel.Monster6
            };

            var selectedTeam = new Team
            {
                Monsters = new List<Monster>()
            };

            foreach (var monsterName in selectedMonsters)
            {
                var monster = viewModel.Team.Find(m => m.MonsterName == monsterName);
                if (monster != null)
                {
                    selectedTeam.Monsters.Add(monster);
                }
            }

            TempData["SelectedTeam"] = JsonConvert.SerializeObject(selectedTeam);

            return RedirectToAction("Battle");
        }
        public IActionResult Battle()
        {
            var selectedTeamJson = TempData["SelectedTeam"]?.ToString();

            if (string.IsNullOrEmpty(selectedTeamJson))
            {
                return RedirectToAction("Play");
            }
            var selectedTeam = JsonConvert.DeserializeObject<Team>(selectedTeamJson);

            return View(selectedTeam);
        }
    }
}