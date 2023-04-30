using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbConstruccionEquipamiento
    {
        public int IdConstruccionEquipamiento { get; set; }
        public int IdEquipamiento { get; set; }
        public int IdConstruccion { get; set; }
        public int Cantidad { get; set; }

        public virtual TbConstruccion IdConstruccionNavigation { get; set; }
        public virtual TbEquipamiento IdEquipamientoNavigation { get; set; }
    }
}
