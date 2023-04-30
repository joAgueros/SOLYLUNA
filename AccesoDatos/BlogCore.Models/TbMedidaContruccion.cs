using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbMedidaContruccion
    {
        public TbMedidaContruccion()
        {
            TbConstruccions = new HashSet<TbConstruccion>();
        }

        public int IdMedidaCon { get; set; }
        public int IdTipoMedida { get; set; }
        public decimal Medida { get; set; }
        public string CodigoIdentificador { get; set; }

        public virtual TbTipoMedida IdTipoMedidaNavigation { get; set; }
        public virtual ICollection<TbConstruccion> TbConstruccions { get; set; }
    }
}
