using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Modelos.Entidades
{
    public class ApplicationUser : IdentityUser
    {

        /*colocar aca las propiedades respectivas a los datos que debe llevar cada usuario*/

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Nombre { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Apellidos { get; set; }
        
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Identificacion { get; set; }

    }
}
