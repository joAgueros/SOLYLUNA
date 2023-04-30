using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbMedium
    {
        public TbMedium()
        {
            TbMediaPropiedades = new HashSet<TbMediaPropiedade>();
            TbMediaWebs = new HashSet<TbMediaWeb>();
        }

        public int IdMedia { get; set; }
        public string Ruta { get; set; }
        public DateTime FechaRegis { get; set; }

        public virtual ICollection<TbMediaPropiedade> TbMediaPropiedades { get; set; }
        public virtual ICollection<TbMediaWeb> TbMediaWebs { get; set; }
    }
}
