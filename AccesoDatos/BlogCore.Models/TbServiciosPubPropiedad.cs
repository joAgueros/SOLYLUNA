using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbServiciosPubPropiedad
    {
        public int IdServicioPubPropiedad { get; set; }
        public int IdPropiedad { get; set; }
        public string Empresa { get; set; }
        public string Estado { get; set; }
        public decimal Distancia { get; set; }
        public decimal Costo { get; set; }
        public int IdServicioPublico { get; set; }
        public string Observacion { get; set; }

        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
        public virtual TbServiciosPub IdServicioPublicoNavigation { get; set; }
    }
}
