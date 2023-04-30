using System;

namespace Modelos.Entidades
{
    public class IntermediarioTabla
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistra { get; set; }
        public int IdPersona { get; set; }
        public string Identificacion { get; set; }
        public string Intermediario { get; set; }
        public string Correo { get; set; }
        public string TipoInter { get; set; }
        public int IdPropiedad { get; set; }
    }
}
