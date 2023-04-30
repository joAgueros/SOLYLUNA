using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbComisione
    {
        public int IdComision { get; set; }
        public int? IdPropiedad { get; set; }
        public string TipoComision { get; set; }
        public decimal PorcentajeTotal { get; set; }
        public decimal PorcentajeOfi { get; set; }
        public string Factura { get; set; }
        public string SobrePrecio { get; set; }
        public decimal MontoSp { get; set; }

        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
