using System.ComponentModel.DataAnnotations;
using ProyectoPrograIV.Models;

namespace ProyectoPrograIV.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Display(Name = "Lista de Amigos")]
        public IEnumerable<FriendViewModel> FriendList { get; set; }

    }
}
