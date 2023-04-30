using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbReferenciasVendedor
    {
        public int IdReferencia { get; set; }
        public int IdClienteV { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public virtual TbClienteVendedor IdClienteVNavigation { get; set; }
    }
}
