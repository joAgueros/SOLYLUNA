using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbResultadoSugef
    {
        public int IdResultado { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }
        public int IdComprador { get; set; }
        public DateTime Fecha { get; set; }

        public virtual TbClienteComprador IdCompradorNavigation { get; set; }
    }
}
