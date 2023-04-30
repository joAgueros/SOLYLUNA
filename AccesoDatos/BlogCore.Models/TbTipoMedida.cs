using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbTipoMedida
    {
        public TbTipoMedida()
        {
            TbMedidaContruccions = new HashSet<TbMedidaContruccion>();
        }

        public int IdTipoMedida { get; set; }
        public string Descripcion { get; set; }
        public string Siglas { get; set; }

        public virtual ICollection<TbMedidaContruccion> TbMedidaContruccions { get; set; }
    }
}
