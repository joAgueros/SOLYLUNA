using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.Entidades;
using System;
using System.Collections.Generic;

namespace Modelos.ViewModels.Propiedades
{
    public class VerPropiedadViewModel
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
        public bool EnBlancoBusqueda { get; set; }
        public bool SinResultados { get; set; }
        public string LinkVideo { get; set; }
        public string Comentario { get; set; }
        public string DescripcionPropiedad { get; set; }
        public string Moneda { get; set; }
        public List<Imagen> ImagenesPropiedad { get; set; }
        public bool VanTodas { get; set; }
        public string BarrioPoblado { get; set; }

    }
}
