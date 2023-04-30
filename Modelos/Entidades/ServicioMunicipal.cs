namespace Modelos.Entidades
{
    public class ServicioMunicipal
    {
        public int IdServicioMunicipal { get; set; }
        public int? IdPropiedad { get; set; }
        public int? IdTipoServicio { get; set; }
        public int? IdCuota { get; set; }
        public decimal? Costo { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }
        public string DescripcionServicio { get; set; }
        public string DescripcionCuota { get; set; }
    }
}
