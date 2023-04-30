using System;
using System.ComponentModel.DataAnnotations;

namespace Modelos.ViewModels.Prueba
{
    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid();
            this.Fecha = DateTime.Now;
        }

        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression(@"^[A-Z a-z0-9ÑñáéíóúÁÉÍÓÚ\\-\\_]+$",
            ErrorMessage = "El Nombre debe ser alfanumérico.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        [StringLength(50)]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [StringLength(1500)]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }
    }
}