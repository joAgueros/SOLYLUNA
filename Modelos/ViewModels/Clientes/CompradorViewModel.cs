using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Modelos.ViewModels.Clientes
{
    public class CompradorViewModel
    {
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Identificacion { get; set; }
        public string TipoIdentificacion { get; set; }
        public string CorreoElectronico { get; set; }
        public string DireccionExacta { get; set; }
        public string TelefonoOficina { get; set; }
        public string TelefonoMovil { get; set; }
        public string TelefonoCasa { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public DateTime FechaRegistra { get; set; }
        public string Estado { get; set; }
        public int IdClienteComprador { get; set; }
        public int IdPersona { get; set; }
        public string PersonaJuridica { get; set; }
        public string CorreoJuridica { get; set; }
        public string CedulaPersonaJuridica { get; set; }
        public string RazonSocial { get; set; }
        public int IdentificacionPersonaJuridica { get; set; }

        public IEnumerable<SelectListItem> TipoPersonas { get; set; }
        public IEnumerable<SelectListItem> Provincias { get; set; }
        public IEnumerable<SelectListItem> Cantones { get; set; }
        public IEnumerable<SelectListItem> Distritos { get; set; }

        public int TipoPersonaId { get; set; }
        public int ProvinciaId { get; set; }
        public int CantonId { get; set; }
        public int DistritoId { get; set; }

    }
}
