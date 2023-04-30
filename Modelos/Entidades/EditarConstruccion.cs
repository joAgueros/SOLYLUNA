namespace Modelos.Entidades
{
    public class EditarConstruccion
    {
        public int IdConstruccion { get; set; }
        public int IdTipoMedida { get; set; }
        public int IdTipoVisualizacion { get; set; }
        public int IdTipoCableado { get; set; }
        public int IdTipoPeriodo { get; set; }
        public int IdTipoEstadoFisico { get; set; }
        public decimal TotalMedida { get; set; }
        public decimal TotalPeriodo { get; set; }
        public string Periodo { get; set; }
        public string EstadoFisico { get; set; }
    }
}
