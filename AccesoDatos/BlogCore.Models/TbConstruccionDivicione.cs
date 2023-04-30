using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbConstruccionDivicione
    {
        public TbConstruccionDivicione()
        {
            TbDivisionMateriales = new HashSet<TbDivisionMateriale>();
        }

        public int IdConsDivisiones { get; set; }
        public int IdConstruccion { get; set; }
        public string Observacion { get; set; }
        public int IdDivision { get; set; }
        public string NombreDescriptivo { get; set; }

        public virtual TbConstruccion IdConstruccionNavigation { get; set; }
        public virtual TbDivisionesObra IdDivisionNavigation { get; set; }
        public virtual ICollection<TbDivisionMateriale> TbDivisionMateriales { get; set; }
    }
}
