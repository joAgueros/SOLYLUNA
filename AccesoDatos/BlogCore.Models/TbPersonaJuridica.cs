using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbPersonaJuridica
    {
        public TbPersonaJuridica()
        {
            TbPersonas = new HashSet<TbPersona>();
        }

        public int IdPersonaJ { get; set; }
        public string NombreEntidad { get; set; }
        public string RazonSocial { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }

        public virtual ICollection<TbPersona> TbPersonas { get; set; }
    }
}
