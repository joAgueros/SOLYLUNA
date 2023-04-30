using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbCaractRequeridasCompradorPropiedad
    {
        public int IdCaracteristica { get; set; }
        public int IdClienteComprador { get; set; }
        public int IdTipoPropiedad { get; set; }
        public string LugarCompra { get; set; }
        public string TienePropiedadEspecifica { get; set; }
        public int IdPropiedad { get; set; }
        public decimal Presupuesto { get; set; }

        public virtual TbClienteComprador IdClienteCompradorNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
        public virtual TbTipoPropiedade IdTipoPropiedadNavigation { get; set; }
    }
}
