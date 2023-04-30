using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Modelos.ViewModels.Propiedades
{
    public class RegistrarPropiedadViewModel
    {
        public int IdPropiedad { get; set; }
        public int IdClienteVendedor { get; set; }
        public string DireccionExacta { get; set; }
        public string NumeroFinca { get; set; }
        public string NumeroPlano { get; set; }
        public decimal PrecioMaximo { get; set; }
        public decimal PrecioMinimo { get; set; }
        public decimal TotalMedida { get; set; }
        public decimal CuotaMantenimiento { get; set; }
        public char DisponeAgua { get; set; }
        public string EstadoAcceso { get; set; }
        public string TipoAcceso { get; set; }
        public string Pozo { get; set; }
        public string EstatusPozo { get; set; }
        public string Intencion { get; set; }
        public string TipoVistada { get; set; }
        public string TopografiaSeleccionada  { get; set; }
        public string NivelCalleSeleccionada { get; set; }
        public string Comentario { get; set; }
        public string DescripcionPropiedad { get; set; }
        public string LinkVideo { get; set; }
        public string Moneda { get; set; }
        public string BarrioPoblado { get; set; }
        public IEnumerable<SelectListItem> TipoPropiedad { get; set; }
        public IEnumerable<SelectListItem> UsoSuelo { get; set; }
        public IEnumerable<SelectListItem> UsoPropiedad { get; set; }
        public IEnumerable<SelectListItem> IngresoPropiedad { get; set; }
        public IEnumerable<SelectListItem> EstadoAccesoPropiedad { get; set; }
        public IEnumerable<SelectListItem> MedidasPropiedad { get; set; }
        public IEnumerable<SelectListItem> TipoPozoPropiedad { get; set; }
        public IEnumerable<SelectListItem> EstatusPozoPropiedad { get; set; }
        public IEnumerable<SelectListItem> TopografiaPropiedad { get; set; }
        public IEnumerable<SelectListItem> NivelCallePropiedad { get; set; }
        public IEnumerable<SelectListItem> Provincias { get; set; }
        public IEnumerable<SelectListItem> Cantones { get; set; }
        public IEnumerable<SelectListItem> Distritos { get; set; }

        public int TipoPropiedadId { get; set; }
        public int UsoSueloId { get; set; }
        public int UsoPropiedadId { get; set; }
        public int IngresoPropiedadId { get; set; }
        public int EstadoAccesoPropiedadId { get; set; }
        public int MedidasPropiedadId { get; set; }
        public int TipoPozosPropiedadId { get; set; }
        public int EstatusPozoPropiedadId { get; set; }
        public int TopografiaId { get; set; }
        public int NivelCalleId { get; set; }
        public int ProvinciaId { get; set; }
        public int CantonId { get; set; }
        public int DistritoId { get; set; }
    }
}
