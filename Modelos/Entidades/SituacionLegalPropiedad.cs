namespace Modelos.Entidades
{
    public class SituacionLegalPropiedad
    {
        public int IdSituacionLegalPropiedad { get; set; }
        public int IdPropiedad { get; set; }
        public int IdTipoSituacion { get; set; }
        public int? IdCuota { get; set; }
        public decimal? Monto { get; set; }
        public string Estado { get; set; }
        public string NombreEntidad { get; set; }
        public string Observacion { get; set; }
        public string DescripcionSituacion { get; set; }
        public string DescripcionCuota { get; set; }
    }
}
