using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class Ubicacion
    {
        public Ubicacion()
        {
            TbPersonas = new HashSet<TbPersona>();
            TbPropiedades = new HashSet<TbPropiedade>();
        }

        public int IdUbicacion { get; set; }
        public double Codigo { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }

        public virtual ICollection<TbPersona> TbPersonas { get; set; }
        public virtual ICollection<TbPropiedade> TbPropiedades { get; set; }
    }
}
