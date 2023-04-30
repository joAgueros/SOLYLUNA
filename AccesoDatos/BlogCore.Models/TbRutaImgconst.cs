using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbRutaImgconst
    {
        public int IdRuta { get; set; }
        public string Ruta { get; set; }
        public int IdConstruccion { get; set; }
        public DateTime FechaIns { get; set; }

        public virtual TbConstruccion IdConstruccionNavigation { get; set; }
    }
}
