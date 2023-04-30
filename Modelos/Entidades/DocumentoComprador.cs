using System;

namespace Modelos.Entidades
{
    public class DocumentoComprador
    {
        public int IdDocumentoComprador { get; set; }
        public int IdComprador { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Estado { get; set; }
        public string Notas { get; set; }
        public string DescripcionDocumento { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaVencimientoString { get; set; }
        public string FechaRegistroString { get; set; }
    }
}
