using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbPropiedadConstruccion
    {
        public int IdPropiedadConstruccion { get; set; }
        public int IdPropiedad { get; set; }
        public int IdConstruccion { get; set; }

        public virtual TbConstruccion IdConstruccionNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
