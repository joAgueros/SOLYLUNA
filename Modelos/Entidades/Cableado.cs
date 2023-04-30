using System.Collections.Generic;

namespace Modelos.Entidades
{
    public class Cableado
    {
        public int IdConstruccionCableado { get; set; }
        public int IdConstruccion { get; set; }
        public List<TiposCableado> Cableados { get; set; }
    }

    public class TiposCableado
    {
        public int IdConstruccionCableado { get; set; }
        public int IdTipoCableado  { get; set; }
        public string Observacion  { get; set; }
        public string EsEntubado  { get; set; }
        public string Nombre { get; set; }
    }
}
