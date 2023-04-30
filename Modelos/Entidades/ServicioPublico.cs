namespace Modelos.Entidades
{
    public class ServicioPublico
    {
        public int IdServicioPublico { get; set; }
        public int IdPropiedad { get; set; }
        public int IdTipoServicio { get; set; }
        public decimal Costo { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }
        public string DescripcionServicio { get; set; }
        public string Empresa { get; set; }
        public decimal Distancia { get; set; }
    }
}
