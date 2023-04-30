using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbMediaPropiedade
    {
        public int IdMedPro { get; set; }
        public int IdPropiedad { get; set; }
        public int IdMedia { get; set; }

        public virtual TbMedium IdMediaNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
    }
}
