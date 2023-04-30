using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbCompradorPropiedade
    {
        public int IdCp { get; set; }
        public int IdClienteC { get; set; }
        public int IdPropiedad { get; set; }
        public DateTime FechaReg { get; set; }

        public virtual TbClienteComprador IdClienteCNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
