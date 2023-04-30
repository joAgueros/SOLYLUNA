using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbDivisionesObra
    {
        public TbDivisionesObra()
        {
            TbConstruccionDiviciones = new HashSet<TbConstruccionDivicione>();
            TbDivisionMateriales = new HashSet<TbDivisionMateriale>();
        }

        public int IdDivision { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbConstruccionDivicione> TbConstruccionDiviciones { get; set; }
        public virtual ICollection<TbDivisionMateriale> TbDivisionMateriales { get; set; }
    }
}
