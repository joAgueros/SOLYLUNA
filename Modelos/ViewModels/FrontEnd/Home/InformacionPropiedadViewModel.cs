using Modelos.Entidades;
using System;
using System.Collections.Generic;

namespace Modelos.ViewModels.FrontEnd.Home
{
    public class InformacionPropiedadViewModel
    {
        public int IdPropiedad { get; set; }
        public string CodigoTipoUsoPropiedad { get; set; }
        public int IdClienteVendedor { get; set; }
        public string DireccionExacta { get; set; }
        public decimal PrecioMaximo { get; set; }
        public decimal PrecioMinimo { get; set; }
        public decimal TotalMedida { get; set; }
        public string Siglas { get; set; }
        public string DescripcionMedida { get; set; }
        public decimal CuotaMantenimiento { get; set; }
        public string DisponeAgua { get; set; }
        public string EstadoAcceso { get; set; }
        public string TipoAcceso { get; set; }
        public string Pozo { get; set; }
        public string EstatusPozo { get; set; }
        public string Intencion { get; set; }
        public string TopografiaSeleccionada { get; set; }
        public string NivelCalleSeleccionada { get; set; }
        public int TotalMeGusta { get; set; }
        public DateTime FechaRegistra { get; set; }
        public string NumeroPlano { get; set; }
        public string NumeroFinca { get; set; }
        public string UsoSuelo { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string TipoPropiedad { get; set; }
        public string UsoPropiedad { get; set; }
        public bool Estado { get; set; }
        public bool Publicado { get; set; }
        public List<Imagen> ImagenesPropiedad { get; set; }
        public List<Construccion> Construcciones { get; set; }
        public List<ServicioMunicipal> ServicioMunicipales { get; set; }
        public List<ServicioPublico> ServiciosPublicos { get; set; }
        public List<PropiedadCaracteristica> PropiedadCaracteristicas { get; set; }
        public List<AccesoPropiedad> RecorridosAcceso { get; set; }
        public List<Avaluo> RecorreAvaluos { get; set; }
        public string DescripcionPropiedad { get; set; }
        public string LinkVideo { get; set; }
        public string Moneda { get; set; }
        public string BarrioPoblado { get; set; }

    }
}
