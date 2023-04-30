using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbPersona
    {
        public TbPersona()
        {
            TbClienteCompradors = new HashSet<TbClienteComprador>();
            TbClienteVendedors = new HashSet<TbClienteVendedor>();
            TbIntermediarios = new HashSet<TbIntermediario>();
            TbUsuariosIns = new HashSet<TbUsuariosIn>();
        }

        public int IdPersona { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public int? IdPersonaJ { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Ape1 { get; set; }
        public string Ape2 { get; set; }
        public string TelPer { get; set; }
        public string TelOfi { get; set; }
        public string TelCas { get; set; }
        public string Email { get; set; }
        public int IdUbicacion { get; set; }
        public string Direccion { get; set; }

        public virtual TbPersonaJuridica IdPersonaJNavigation { get; set; }
        public virtual TbTipoIdentificacion IdTipoIdentificacionNavigation { get; set; }
        public virtual Ubicacion IdUbicacionNavigation { get; set; }
        public virtual ICollection<TbClienteComprador> TbClienteCompradors { get; set; }
        public virtual ICollection<TbClienteVendedor> TbClienteVendedors { get; set; }
        public virtual ICollection<TbIntermediario> TbIntermediarios { get; set; }
        public virtual ICollection<TbUsuariosIn> TbUsuariosIns { get; set; }
    }
}
