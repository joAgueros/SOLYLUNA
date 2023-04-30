using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbCaracteristica
    {
        public TbCaracteristica()
        {
            TbContruccionCaracteristicas = new HashSet<TbContruccionCaracteristica>();
            TbPropiedadCaracteristicas = new HashSet<TbPropiedadCaracteristica>();
        }

        public int IdCaracteristica { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbContruccionCaracteristica> TbContruccionCaracteristicas { get; set; }
        public virtual ICollection<TbPropiedadCaracteristica> TbPropiedadCaracteristicas { get; set; }
    }
}
