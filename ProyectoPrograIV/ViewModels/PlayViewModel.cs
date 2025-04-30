using ProyectoPrograIV.Models;

namespace ProyectoPrograIV.ViewModels
{
    public class PlayViewModel
    {
        public List<string> MonsterNames { get; set; } = new List<string> { "", "", "", "", "", "" };

        public List<Monster> Team { get; set; } = new List<Monster>();  
    }
}
