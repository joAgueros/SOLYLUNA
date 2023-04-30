using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbLegalPropiedad
    {
        public int IdLegal { get; set; }
        public int IdPropiedad { get; set; }
        public int IdTipoSituacion { get; set; }
        public string NombreEntidad { get; set; }
        public int? IdCuota { get; set; }
        public decimal? Monto { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }

        public virtual TbTipoCuota IdCuotaNavigation { get; set; }
        public virtual TbPropiedade IdPropiedadNavigation { get; set; }
        public virtual TbTipoSituacion IdTipoSituacionNavigation { get; set; }
    }
}
