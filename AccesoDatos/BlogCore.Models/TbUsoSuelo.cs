using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbUsoSuelo
    {
        public TbUsoSuelo()
        {
            TbPropiedades = new HashSet<TbPropiedade>();
        }

        public int IdUsoSuelo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbPropiedade> TbPropiedades { get; set; }
    }
}
