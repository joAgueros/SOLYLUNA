using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Entidades
{
    public class Avaluo
    {
        public int IdAvaluo { get; set; }
        public int IdConstruccion { get; set; }
        public int IdPropiedad{ get; set; }
        public string Monto { get; set; }
        public DateTime FechaAvaluo { get; set; }

    }
}
