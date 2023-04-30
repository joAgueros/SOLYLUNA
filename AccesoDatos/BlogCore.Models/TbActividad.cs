using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbActividad
    {
        public TbActividad()
        {
            TbAgendaPropiedads = new HashSet<TbAgendaPropiedad>();
        }

        public int IdActividad { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbAgendaPropiedad> TbAgendaPropiedads { get; set; }
    }
}
