using System;
using System.Collections.Generic;

#nullable disable

namespace AccesoDatos.BlogCore.Models
{
    public partial class TbContacto
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Tel { get; set; }
        public string Correo { get; set; }
        public string TipoContacto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
    }
}
