using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbMaterialesObra
    {
        public TbMaterialesObra()
        {
            TbDivisionMateriales = new HashSet<TbDivisionMateriale>();
        }

        public int IdMaterial { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbDivisionMateriale> TbDivisionMateriales { get; set; }
    }
}
