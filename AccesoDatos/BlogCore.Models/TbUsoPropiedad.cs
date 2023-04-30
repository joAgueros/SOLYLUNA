using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbUsoPropiedad
    {
        public TbUsoPropiedad()
        {
            TbUsoTipopropiedades = new HashSet<TbUsoTipopropiedade>();
        }

        public int IdUsoPro { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbUsoTipopropiedade> TbUsoTipopropiedades { get; set; }
    }
}
