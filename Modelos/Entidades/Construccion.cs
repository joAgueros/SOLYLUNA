using System.Collections.Generic;

namespace Modelos.Entidades
{
    public class Construccion
    {
        public string Descripcion { get; set; }
        public int IdPropiedad { get; set; }
        public int IdConstruccion { get; set; }
        public string FechaRegistra { get; set; }
        public string Estado { get; set; }
        public decimal Medida { get; set; }
        public string TipoMedida { get; set; }
        public decimal TotalMedida { get; set; }
        public decimal TotalPeriodo { get; set; }
        public string Siglas { get; set; }
        public string Vistada  { get; set; }
        public string TipoEntubado { get; set; }
        public string TipoCableado { get; set; }
        public string Antiguedad { get; set; }
        public string EstadoFisico { get; set; }
        public int? IdEntubado { get; set; }
        public int? IdVisualizacion { get; set; }
        public int? IdMedida { get; set; }
        public int? IdEstadoFisico { get; set; }
        public int? IdCableado { get; set; }
        public int? IdPeriodo { get; set; }

    }
}
