using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbUsuariosIn
    {
        public int IdUsuarioIn { get; set; }
        public int IdPersona { get; set; }
        public string Role { get; set; }
        public string Estado { get; set; }
        public DateTime? FechIngreso { get; set; }
        public string Puesto { get; set; }

        public virtual TbPersona IdPersonaNavigation { get; set; }
    }
}
