using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbTipoCuota
    {
        public TbTipoCuota()
        {
            TbLegalPropiedads = new HashSet<TbLegalPropiedad>();
            TbServiciosMunicipales = new HashSet<TbServiciosMunicipale>();
        }

        public int IdCuota { get; set; }
        public string Cuota { get; set; }

        public virtual ICollection<TbLegalPropiedad> TbLegalPropiedads { get; set; }
        public virtual ICollection<TbServiciosMunicipale> TbServiciosMunicipales { get; set; }
    }
}
