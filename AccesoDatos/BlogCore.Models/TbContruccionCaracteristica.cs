using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbContruccionCaracteristica
    {
        public int IdConstruccionCaracteristica { get; set; }
        public int IdConstruccion { get; set; }
        public int IdCaracteristica { get; set; }
        public int Cantidad { get; set; }

        public virtual TbCaracteristica IdCaracteristicaNavigation { get; set; }
        public virtual TbConstruccion IdConstruccionNavigation { get; set; }
    }
}
