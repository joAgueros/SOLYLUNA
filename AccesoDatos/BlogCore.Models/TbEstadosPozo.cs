using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbEstadosPozo
    {
        public TbEstadosPozo()
        {
            TbPropiedades = new HashSet<TbPropiedade>();
        }

        public int IdEstadosPozo { get; set; }
        public string Pozo { get; set; }
        public string EstadoLegal { get; set; }
        public string CodigoIdentificador { get; set; }

        public virtual ICollection<TbPropiedade> TbPropiedades { get; set; }
    }
}
