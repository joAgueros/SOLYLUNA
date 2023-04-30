using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbContratoDetalle
    {
        public int IdContrato { get; set; }
        public int? IdPropiedad { get; set; }
        public string Exclusivo { get; set; }
        public string Directo { get; set; }
        public DateTime? FechaContratato { get; set; }
        public DateTime? Vencimiento { get; set; }

        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
