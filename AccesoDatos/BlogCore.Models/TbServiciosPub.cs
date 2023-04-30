using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbServiciosPub
    {
        public TbServiciosPub()
        {
            TbServiciosPubPropiedads = new HashSet<TbServiciosPubPropiedad>();
        }

        public int IdServicioPublico { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbServiciosPubPropiedad> TbServiciosPubPropiedads { get; set; }
    }
}
