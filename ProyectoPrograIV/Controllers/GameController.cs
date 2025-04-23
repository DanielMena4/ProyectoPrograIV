using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIV.ViewModels;

namespace ProyectoPrograIV.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Play()
        {
            return View();
        }
    }
}
