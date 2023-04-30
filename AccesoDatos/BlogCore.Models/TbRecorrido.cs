using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbRecorrido
    {
        public int IdPropiedad { get; set; }
        public int IdAccesibilidad { get; set; }
        public decimal RecorridoKm { get; set; }
        public string Descripcion { get; set; }
        public int IdRecorrido { get; set; }

        public virtual TbAccesibilidad IdAccesibilidadNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
