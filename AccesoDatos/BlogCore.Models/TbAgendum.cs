using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbAgendum
    {
        public TbAgendum()
        {
            TbAgendaOficinas = new HashSet<TbAgendaOficina>();
            TbAgendaPropiedads = new HashSet<TbAgendaPropiedad>();
        }

        public int IdAgenda { get; set; }
        public string NombreUsuario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Final { get; set; }
        public bool DiaCompleto { get; set; }
        public string Color { get; set; }

        public virtual ICollection<TbAgendaOficina> TbAgendaOficinas { get; set; }
        public virtual ICollection<TbAgendaPropiedad> TbAgendaPropiedads { get; set; }
    }
}
