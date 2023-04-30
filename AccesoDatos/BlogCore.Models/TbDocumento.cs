using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbDocumento
    {
        public TbDocumento()
        {
            TbDocumentosCompradors = new HashSet<TbDocumentosComprador>();
            TbDocumentosPropiedads = new HashSet<TbDocumentosPropiedad>();
        }

        public int IdDocumento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbDocumentosComprador> TbDocumentosCompradors { get; set; }
        public virtual ICollection<TbDocumentosPropiedad> TbDocumentosPropiedads { get; set; }
    }
}
