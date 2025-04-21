using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograIV.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Se requiere Nombre de usuario")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Se requiere Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Se requiere Contraseña")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "La {0} debe ser por lo menos {2} y como maximo {1} caracteres de largo")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "La contraseña y la confirmación de contraseña no coinciden.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Se requiere Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }

    }
}
