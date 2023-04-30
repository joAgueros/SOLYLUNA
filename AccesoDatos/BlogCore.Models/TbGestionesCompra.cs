using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbGestionesCompra
    {
        public int IdGestion { get; set; }
        public int IdComprador { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }

        public virtual TbClienteComprador IdCompradorNavigation { get; set; }
    }
}
