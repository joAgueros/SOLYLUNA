using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbAcceso
    {
        public TbAcceso()
        {
            TbPropiedades = new HashSet<TbPropiedade>();
        }

        public int IdAcceso { get; set; }
        public string TipoAcceso { get; set; }
        public string Estado { get; set; }
        public string CodigoIdentificador { get; set; }

        public virtual ICollection<TbPropiedade> TbPropiedades { get; set; }
    }
}
