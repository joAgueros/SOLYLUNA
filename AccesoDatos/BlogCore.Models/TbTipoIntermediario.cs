using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbTipoIntermediario
    {
        public TbTipoIntermediario()
        {
            TbIntermediarios = new HashSet<TbIntermediario>();
        }

        public int IdTipoInter { get; set; }
        public string Detalle { get; set; }

        public virtual ICollection<TbIntermediario> TbIntermediarios { get; set; }
    }
}
