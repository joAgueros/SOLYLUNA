using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbServiciosMunicipale
    {
        public int IdSerMuni { get; set; }
        public int? IdPropiedad { get; set; }
        public int? IdTipoSer { get; set; }
        public decimal? Costo { get; set; }
        public int? IdCuota { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }

        public virtual TbTipoCuota IdCuotaNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
        public virtual TbTiposerMunicipal IdTipoSerNavigation { get; set; }
    }
}
