using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos.ViewModels.Clientes
{
    public class IntermediarioViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obigatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Nombre Completo")]

        [StringLength(30,ErrorMessage ="Deben ser menos de 30 caractéres")]
        public string NombreCompleto { get; set; }
        [StringLength(30, ErrorMessage = "Deben ser menos de 30 caractéres")]
        public string Apellido1 { get; set; }
        [StringLength(30, ErrorMessage = "Deben ser menos de 30 caractéres")]
        public string Apellido2 { get; set; }
        [Display(Name = "Identificación")]
        [MaxLength(9, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        public string Identificacion { get; set; }
        public string TipoIdentificacion { get; set; }
        public string CorreoElectronico { get; set; }
        public string DireccionExacta { get; set; }
        [Display(Name = "Teléfono Oficina")]
        [MaxLength(9, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        public string TelefonoOficina { get; set; }

        [Required]
        [Display(Name = "Teléfono Móvil")]
        [MaxLength(9, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        public string TelefonoMovil { get; set; }
        
        [Display(Name = "Teléfono Casa")]
        [MaxLength(9, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        public string TelefonoCasa { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public DateTime FechaRegistra { get; set; }
        public string Estado { get; set; }
        public int IdIntermediario { get; set; }
        public string DetalleIntermediario { get; set; }
        public int IdPersona { get; set; }
        public string PersonaJuridica { get; set; }
        public int IdentificacionPersonaJuridica { get; set; }

        public IEnumerable<SelectListItem> TipoPersonas { get; set; }
        public IEnumerable<SelectListItem> Provincias { get; set; }
        public IEnumerable<SelectListItem> Cantones { get; set; }
        public IEnumerable<SelectListItem> Distritos { get; set; }
        public IEnumerable<SelectListItem> TipoIntermediarios { get; set; }

        public int TipoPersonaId { get; set; }
        public int ProvinciaId { get; set; }
        public int CantonId { get; set; }
        public int DistritoId { get; set; }
        public int IdtipoIntermediario { get; set; }
    }
}

