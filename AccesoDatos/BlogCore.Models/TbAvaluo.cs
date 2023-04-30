using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbAvaluo
    {
        public int IdAvaluo { get; set; }
        public int? IdConstruccion { get; set; }
        public int? IdPropiedad { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaAvaluo { get; set; }

        public virtual TbConstruccion IdConstruccionNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
