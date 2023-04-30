namespace Modelos.Entidades
{
    public class AccesoPropiedad
    {
        public int IdRecorrido { get; set; }
        public int IdTipoAccesibilidad { get; set; }
        public int IdPropiedad { get; set; }
        public decimal Recorrido { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionAccesibilidad { get; set; }
    }
}
