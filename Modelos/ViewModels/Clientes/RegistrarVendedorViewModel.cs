using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Modelos.ViewModels.Clientes
{
    public class RegistrarVendedorViewModel
    { 
        [Required (ErrorMessage = "El campo {0} es obigatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }

     
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Dirección exacta")]
        public string DireccionExacta { get; set; }

        [Display(Name = "Teléfono Oficina")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        public string TelefonoOficina { get; set; }

        [Required]
        [Display(Name = "Teléfono Móvil")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        public string TelefonoMovil { get; set; }

        [Display(Name = "Teléfono Casa")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        public string TelefonoCasa { get; set; }


        public IEnumerable<SelectListItem> TipoPersonas { get; set; }
        public IEnumerable<SelectListItem> Provincias { get; set; }
        public IEnumerable<SelectListItem> Cantones { get; set; }
        public IEnumerable<SelectListItem> Distritos { get; set; }


        public int IdPersonaJu { get; set; }
        [Display(Name = "Tipo Persona")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar el tipo de persona")]
        public int TipoPersonaId { get; set; }
        [Display(Name = "Nombre Jurídico")]
        public string NombreEntidad { get; set; }
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }
        [Display(Name = "Cédula Jurídica")]
        public string CedulaJu { get; set; }

        [MaxLength(30, ErrorMessage = "El campo {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        public string CorreoJu { get; set; }

        [Display(Name = "Provincia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una provincia")]
        public int ProvinciaId { get; set; }

        [Display(Name = "Cantón")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cantón")]
        public int CantonId { get; set; }

        [Display(Name = "Distrito")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un distrito")]
        public int DistritoId { get; set; }

    }
}
