using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbTipoIdentificacion
    {
        public TbTipoIdentificacion()
        {
            TbPersonas = new HashSet<TbPersona>();
        }

        public int IdTipoIdentificacion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<TbPersona> TbPersonas { get; set; }
    }
}
