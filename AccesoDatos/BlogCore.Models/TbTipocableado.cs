using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbTipocableado
    {
        public TbTipocableado()
        {
            TbConstruccionCableados = new HashSet<TbConstruccionCableado>();
        }

        public int IdTipoCableado { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbConstruccionCableado> TbConstruccionCableados { get; set; }
    }
}
