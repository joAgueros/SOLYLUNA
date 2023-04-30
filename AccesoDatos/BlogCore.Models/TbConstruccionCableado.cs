using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbConstruccionCableado
    {
        public int IdConstruccionCableado { get; set; }
        public string Entubado { get; set; }
        public int IdTipoCableado { get; set; }
        public int? IdConstruccion { get; set; }
        public string Observacion { get; set; }

        public virtual TbConstruccion IdConstruccionNavigation { get; set; }
        public virtual TbTipocableado IdTipoCableadoNavigation { get; set; }
    }
}
