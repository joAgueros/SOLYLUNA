using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbClienteVendedor
    {
        public TbClienteVendedor()
        {
            TbPropiedades = new HashSet<TbPropiedade>();
            TbReferenciasVendedors = new HashSet<TbReferenciasVendedor>();
        }

        public int IdClienteV { get; set; }
        public int IdPersona { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegis { get; set; }

        public virtual TbPersona IdPersonaNavigation { get; set; }
        public virtual ICollection<TbPropiedade> TbPropiedades { get; set; }
        public virtual ICollection<TbReferenciasVendedor> TbReferenciasVendedors { get; set; }
    }
}
