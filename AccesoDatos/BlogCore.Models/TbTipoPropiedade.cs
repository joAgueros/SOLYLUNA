using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbTipoPropiedade
    {
        public TbTipoPropiedade()
        {
            TbCaractRequeridasCompradorPropiedads = new HashSet<TbCaractRequeridasCompradorPropiedad>();
            TbUsoTipopropiedades = new HashSet<TbUsoTipopropiedade>();
        }

        public int IdTipoPro { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbCaractRequeridasCompradorPropiedad> TbCaractRequeridasCompradorPropiedads { get; set; }
        public virtual ICollection<TbUsoTipopropiedade> TbUsoTipopropiedades { get; set; }
    }
}
