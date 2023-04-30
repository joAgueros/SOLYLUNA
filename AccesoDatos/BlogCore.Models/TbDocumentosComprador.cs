using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbDocumentosComprador
    {
        public int IdDocComp { get; set; }
        public int IdDocumento { get; set; }
        public int IdComprador { get; set; }
        public string EstadoRecepcion { get; set; }
        public DateTime? Vencimiento { get; set; }
        public DateTime FechaRegis { get; set; }
        public string Notas { get; set; }

        public virtual TbClienteComprador IdCompradorNavigation { get; set; }
        public virtual TbDocumento IdDocumentoNavigation { get; set; }
    }
}
