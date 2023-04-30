using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbMedidaPropiedad
    {
        public TbMedidaPropiedad()
        {
            TbPropiedades = new HashSet<TbPropiedade>();
        }

        public int IdMedidaPro { get; set; }
        public int IdTipoMedida { get; set; }
        public decimal Medida { get; set; }
        public string CodigoIdentificador { get; set; }

        public virtual ICollection<TbPropiedade> TbPropiedades { get; set; }
    }
}
