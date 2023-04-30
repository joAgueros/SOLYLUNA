using System;

namespace Modelos.Entidades
{
    public class ResultadoSugef
    {
        public int Id { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroString { get; set; }
        public string Estado { get; set; }
        public int IdComprador { get; set; }
    }
}
