using System;

namespace Modelos.Entidades
{
    public class CompradorTabla
    {

        public int Id { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistra { get; set; }
        public int IdPersona { get; set; }
        public string Identificacion { get; set; }
        public string Comprador { get; set; }
        public string Correo { get; set; }

    }
}
