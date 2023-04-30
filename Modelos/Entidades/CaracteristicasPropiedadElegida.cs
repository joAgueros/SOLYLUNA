namespace Modelos.Entidades
{
    public class CaracteristicasPropiedadElegida
    {
        public int Id { get; set; }
        public int IdComprador { get; set; }
        public int IdTipoPropiedad { get; set; }
        public string Lugar { get; set; }
        public string CodigoPropiedad { get; set; }
        public decimal Presupuesto { get; set; }
        public string PoseePropiedadEspecifica { get; set; }
        public string NompreTipoPropiedad { get; set; }
    }
}
