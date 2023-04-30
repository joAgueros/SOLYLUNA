using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbAccesibilidad
    {
        public TbAccesibilidad()
        {
            TbRecorridos = new HashSet<TbRecorrido>();
        }

        public int IdAccesibilidad { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbRecorrido> TbRecorridos { get; set; }
    }
}
