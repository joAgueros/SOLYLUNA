using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbIntermediario
    {
        public TbIntermediario()
        {
            TbIntermediarioPropiedads = new HashSet<TbIntermediarioPropiedad>();
        }

        public int IdIntermediario { get; set; }
        public int? IdTipoInter { get; set; }
        public int? IdPersona { get; set; }
        public string Estado { get; set; }

        public virtual TbPersona IdPersonaNavigation { get; set; }
        public virtual TbTipoIntermediario IdTipoInterNavigation { get; set; }
        public virtual ICollection<TbIntermediarioPropiedad> TbIntermediarioPropiedads { get; set; }
    }
}
