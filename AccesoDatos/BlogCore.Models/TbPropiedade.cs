using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbPropiedade
    {
        public TbPropiedade()
        {
            TbAgendaPropiedads = new HashSet<TbAgendaPropiedad>();
            TbAvaluos = new HashSet<TbAvaluo>();
            TbCaractRequeridasCompradorPropiedads = new HashSet<TbCaractRequeridasCompradorPropiedad>();
            TbComisiones = new HashSet<TbComisione>();
            TbCompradorPropiedades = new HashSet<TbCompradorPropiedade>();
            TbContratoDetalles = new HashSet<TbContratoDetalle>();
            TbDocumentosPropiedads = new HashSet<TbDocumentosPropiedad>();
            TbIntermediarioPropiedads = new HashSet<TbIntermediarioPropiedad>();
            TbLegalPropiedads = new HashSet<TbLegalPropiedad>();
            TbMediaPropiedades = new HashSet<TbMediaPropiedade>();
            TbPropiedadCaracteristicas = new HashSet<TbPropiedadCaracteristica>();
            TbPropiedadConstruccions = new HashSet<TbPropiedadConstruccion>();
            TbRecorridos = new HashSet<TbRecorrido>();
            TbRutaImgprops = new HashSet<TbRutaImgprop>();
            TbServiciosMunicipales = new HashSet<TbServiciosMunicipale>();
            TbServiciosPubPropiedads = new HashSet<TbServiciosPubPropiedad>();
        }

        public int IdPropiedad { get; set; }
        public string CodigoIdentificador { get; set; }
        public int? IdClienteV { get; set; }
        public int IdUsoTipo { get; set; }
        public string CodigoTipoUsoPropiedad { get; set; }
        public int IdUbicacion { get; set; }
        public string Direccion { get; set; }
        public string BarrioPoblado { get; set; }
        public int? IdUsoSuelo { get; set; }
        public int? IdMedidaPro { get; set; }
        public string Topografia { get; set; }
        public int? IdAcceso { get; set; }
        public int? IdEstadosPozo { get; set; }
        public string DisAgua { get; set; }
        public string NumPlano { get; set; }
        public decimal? CuotaMante { get; set; }
        public decimal? PrecioMax { get; set; }
        public decimal? PrecioMin { get; set; }
        public string NivelCalle { get; set; }
        public string NumFinca { get; set; }
        public DateTime FechaRegis { get; set; }
        public int Megusta { get; set; }
        public string Intencion { get; set; }
        public string Descripcion { get; set; }
        public string LinkVideo { get; set; }
        public string Moneda { get; set; }
        public string PoseeVistaMar { get; set; }
        public string PoseeVistaMontania { get; set; }
        public string PoseeVistaValle { get; set; }
        public string SinVista { get; set; }
        public string Estado { get; set; }
        public string Publicado { get; set; }
        public string Eliminado { get; set; }
        public string DireccionCompleta { get; set; }

        public virtual TbAcceso IdAccesoNavigation { get; set; }
        public virtual TbClienteVendedor IdClienteVNavigation { get; set; }
        public virtual TbEstadosPozo IdEstadosPozoNavigation { get; set; }
        public virtual TbMedidaPropiedad IdMedidaProNavigation { get; set; }
        public virtual Ubicacion IdUbicacionNavigation { get; set; }
        public virtual TbUsoSuelo IdUsoSueloNavigation { get; set; }
        public virtual TbUsoTipopropiedade IdUsoTipoNavigation { get; set; }
        public virtual ICollection<TbAgendaPropiedad> TbAgendaPropiedads { get; set; }
        public virtual ICollection<TbAvaluo> TbAvaluos { get; set; }
        public virtual ICollection<TbCaractRequeridasCompradorPropiedad> TbCaractRequeridasCompradorPropiedads { get; set; }
        public virtual ICollection<TbComisione> TbComisiones { get; set; }
        public virtual ICollection<TbCompradorPropiedade> TbCompradorPropiedades { get; set; }
        public virtual ICollection<TbContratoDetalle> TbContratoDetalles { get; set; }
        public virtual ICollection<TbDocumentosPropiedad> TbDocumentosPropiedads { get; set; }
        public virtual ICollection<TbIntermediarioPropiedad> TbIntermediarioPropiedads { get; set; }
        public virtual ICollection<TbLegalPropiedad> TbLegalPropiedads { get; set; }
        public virtual ICollection<TbMediaPropiedade> TbMediaPropiedades { get; set; }
        public virtual ICollection<TbPropiedadCaracteristica> TbPropiedadCaracteristicas { get; set; }
        public virtual ICollection<TbPropiedadConstruccion> TbPropiedadConstruccions { get; set; }
        public virtual ICollection<TbRecorrido> TbRecorridos { get; set; }
        public virtual ICollection<TbRutaImgprop> TbRutaImgprops { get; set; }
        public virtual ICollection<TbServiciosMunicipale> TbServiciosMunicipales { get; set; }
        public virtual ICollection<TbServiciosPubPropiedad> TbServiciosPubPropiedads { get; set; }
    }
}
