using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograIV.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Se requiere Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
