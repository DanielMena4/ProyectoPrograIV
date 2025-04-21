using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograIV.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Se requiere Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Se requiere Contraseña")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "La {0} debe ser por lo menos {2} y como maximo {1} caracteres de largo")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = "La contraseña y la confirmación de contraseña no coinciden.")]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Se requiere Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva Contraseña")]
        public string ConfirmNewPassword { get; set; }
    }
}
