using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbUsoTipopropiedade
    {
        public TbUsoTipopropiedade()
        {
            TbPropiedades = new HashSet<TbPropiedade>();
        }

        public int IdUsoTipo { get; set; }
        public int IdUsoPro { get; set; }
        public int IdTipoPro { get; set; }
        public string CodigoIdentificador { get; set; }

        public virtual TbUsoPropiedad IdTipoPro1 { get; set; }
        public virtual TbTipoPropiedade IdTipoProNavigation { get; set; }
        public virtual ICollection<TbPropiedade> TbPropiedades { get; set; }
    }
}
