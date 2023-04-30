using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbClienteComprador
    {
        public TbClienteComprador()
        {
            TbCaractRequeridasCompradorPropiedads = new HashSet<TbCaractRequeridasCompradorPropiedad>();
            TbCompradorPropiedades = new HashSet<TbCompradorPropiedade>();
            TbDocumentosCompradors = new HashSet<TbDocumentosComprador>();
            TbGestionesCompras = new HashSet<TbGestionesCompra>();
            TbReferenciasCompradors = new HashSet<TbReferenciasComprador>();
            TbResultadoSolicitantes = new HashSet<TbResultadoSolicitante>();
            TbResultadoSugefs = new HashSet<TbResultadoSugef>();
        }

        public int IdClienteC { get; set; }
        public int IdPersona { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegis { get; set; }

        public virtual TbPersona IdPersonaNavigation { get; set; }
        public virtual ICollection<TbCaractRequeridasCompradorPropiedad> TbCaractRequeridasCompradorPropiedads { get; set; }
        public virtual ICollection<TbCompradorPropiedade> TbCompradorPropiedades { get; set; }
        public virtual ICollection<TbDocumentosComprador> TbDocumentosCompradors { get; set; }
        public virtual ICollection<TbGestionesCompra> TbGestionesCompras { get; set; }
        public virtual ICollection<TbReferenciasComprador> TbReferenciasCompradors { get; set; }
        public virtual ICollection<TbResultadoSolicitante> TbResultadoSolicitantes { get; set; }
        public virtual ICollection<TbResultadoSugef> TbResultadoSugefs { get; set; }
    }
}
