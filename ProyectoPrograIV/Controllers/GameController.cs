using Microsoft.AspNetCore.Mvc;

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
