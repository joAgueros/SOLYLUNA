using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbEquipamiento
    {
        public TbEquipamiento()
        {
            TbConstruccionEquipamientos = new HashSet<TbConstruccionEquipamiento>();
        }

        public int IdEquipamiento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbConstruccionEquipamiento> TbConstruccionEquipamientos { get; set; }
    }
}
