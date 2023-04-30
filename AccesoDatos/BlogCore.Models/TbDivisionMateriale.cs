using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbDivisionMateriale
    {
        public int IdDivisionMateriales { get; set; }
        public int IdDivision { get; set; }
        public int IdMaterial { get; set; }
        public string Descripcion { get; set; }
        public int IdConsDivisiones { get; set; }

        public virtual TbConstruccionDivicione IdConsDivisionesNavigation { get; set; }
        public virtual TbDivisionesObra IdDivisionNavigation { get; set; }
        public virtual TbMaterialesObra IdMaterialNavigation { get; set; }
    }
}
