using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbAgendaPropiedad
    {
        public int IdAgendaProp { get; set; }
        public int IdAgenda { get; set; }
        public int IdPropiedad { get; set; }

        public virtual TbPropiedade IdAgenda1 { get; set; }
        public virtual TbAgendum IdAgendaNavigation { get; set; }
        public virtual TbActividad IdPropiedadNavigation { get; set; }
    }
}
