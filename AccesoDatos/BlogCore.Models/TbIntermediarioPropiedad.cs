using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbIntermediarioPropiedad
    {
        public int IdInterProp { get; set; }
        public int IdIntermediario { get; set; }
        public int IdPropiedad { get; set; }
        public DateTime FechaRegis { get; set; }

        public virtual TbIntermediario IdIntermediarioNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
