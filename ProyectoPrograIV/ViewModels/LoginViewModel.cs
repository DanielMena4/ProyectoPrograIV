using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograIV.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Se requiere Contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Se requiere Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Recordar contraseña")]
        public bool RememberMe { get; set; }
    }
}
