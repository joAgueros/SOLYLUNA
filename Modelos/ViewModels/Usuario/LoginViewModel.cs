using System.ComponentModel.DataAnnotations;

namespace Modelos.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string NombreUsuario { get; set; }

        [Required]
        [MinLength(6)]
        public string Contrasenia { get; set; }

        public bool Recordatorio { get; set; }
    }
}
