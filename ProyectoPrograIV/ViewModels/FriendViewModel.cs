using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograIV.ViewModels
{
    public class FriendViewModel
    {
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
    }
}
