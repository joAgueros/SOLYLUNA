using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbAgendaOficina
    {
        public int IdAgendaOficina { get; set; }
        public int IdAgenda { get; set; }
        public string Estado { get; set; }

        public virtual TbAgendum IdAgendaNavigation { get; set; }
    }
}
