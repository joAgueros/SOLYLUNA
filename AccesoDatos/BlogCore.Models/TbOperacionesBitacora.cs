using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbOperacionesBitacora
    {
        public TbOperacionesBitacora()
        {
            TbBitacoras = new HashSet<TbBitacora>();
        }

        public int IdOperacion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbBitacora> TbBitacoras { get; set; }
    }
}
