using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbPropiedadCaracteristica
    {
        public int IdPropiedadCaracteristica { get; set; }
        public int IdPropiedad { get; set; }
        public int IdCaracteristica { get; set; }
        public int Cantidad { get; set; }

        public virtual TbCaracteristica IdCaracteristicaNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
