using System.ComponentModel.DataAnnotations;

namespace Modelos.ViewModels.Usuario
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Contraseña actual")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres.")]
        public string ViejaContrasenia { get; set; }

        [Display(Name = "Nueva Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres.")]
        public string NuevaContrasenia { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres.")]
        [Compare("NuevaContrasenia", ErrorMessage = "La contraseña y la confirmación no coinciden")]
        public string ConfirmacionContrasenia { get; set; }
    }
}
