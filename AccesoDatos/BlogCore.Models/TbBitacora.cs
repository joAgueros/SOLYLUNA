using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbBitacora
    {
        public int IdBitacora { get; set; }
        public string IdUsuario { get; set; }
        public int IdOperacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string TablaAfectada { get; set; }
        public string IdTablaAfectada { get; set; }

        public virtual TbOperacionesBitacora IdOperacionNavigation { get; set; }
    }
}
