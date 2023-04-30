using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbRutaImgprop
    {
        public int IdRuta { get; set; }
        public int IdPropiedad { get; set; }
        public string Ruta { get; set; }
        public DateTime FechaIns { get; set; }

        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
