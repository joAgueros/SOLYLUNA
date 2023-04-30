namespace Modelos.Entidades
{
    public class Bitacora
    {
        public int IdBitacora { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public string TipoOperacion { get; set; }
        public string FechaString { get; set; }
        public string HoraString { get; set; }
        public string FechaConcantenada { get; set; }
        public string RegistroAfectado { get; set; }
        public string IdRegistroAfectado { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}
