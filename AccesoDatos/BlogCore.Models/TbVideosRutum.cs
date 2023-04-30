using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbVideosRutum
    {
        public int IdVideo { get; set; }
        public int IdPropiedad { get; set; }
        public string Ruta { get; set; }
        public DateTime FechaIns { get; set; }
    }
}
