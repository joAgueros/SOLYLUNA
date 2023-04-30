using System.ComponentModel.DataAnnotations;

namespace Modelos.ViewModels.Usuario
{
    public class EditarUsuarioSistemaViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obigatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Primer Apellido")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }
        public string Correo { get; set; }
        public bool Activa { get; set; }

    }
}
