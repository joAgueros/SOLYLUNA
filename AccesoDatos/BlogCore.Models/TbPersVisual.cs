using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbPersVisual
    {
        public TbPersVisual()
        {
            TbConstruccions = new HashSet<TbConstruccion>();
        }

        public int IdPerVisual { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbConstruccion> TbConstruccions { get; set; }
    }
}
