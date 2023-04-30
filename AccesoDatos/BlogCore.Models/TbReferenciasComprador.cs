using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbReferenciasComprador
    {
        public int IdReferencia { get; set; }
        public int IdClienteC { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public virtual TbClienteComprador IdClienteCNavigation { get; set; }
    }
}
