using System.ComponentModel.DataAnnotations;

namespace Modelos.ViewModels.Usuario
{
    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el correo")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo con formato válido")]
        public string Email { get; set; }
    }
}
