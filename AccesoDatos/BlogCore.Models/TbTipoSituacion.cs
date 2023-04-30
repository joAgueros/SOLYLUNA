using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbTipoSituacion
    {
        public TbTipoSituacion()
        {
            TbLegalPropiedads = new HashSet<TbLegalPropiedad>();
        }

        public int IdTipoSituacion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbLegalPropiedad> TbLegalPropiedads { get; set; }
    }
}
