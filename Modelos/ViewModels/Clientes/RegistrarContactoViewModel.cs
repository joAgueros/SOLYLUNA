using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Modelos.ViewModels.Clientes
{
    public class RegistrarContactoViewModel
    {
        public int IdContacto { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage = "No te olvides del Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "No te olvides del apellido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "No te olvides del teléfono")]
        public string Tel { get; set; }
        [Required]
        public string Correo { get; set; }
        public string TipoContacto { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public DateTime Fecha { set; get; }
        public char Estado { get; set; }
}
}
