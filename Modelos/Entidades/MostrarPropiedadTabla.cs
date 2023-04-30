namespace Modelos.Entidades
{
    public class MostrarPropiedadTabla
    {
        public int Id { get; set; }
        public int? IdClienteVenta { get; set; }
        public string NombreClienteV{ get; set; }
        public string CodigoTipoUsoPropiedad { get; set; }
        public string TipoPropiedad { get; set; }
        public string UsoPropiedad { get; set; }
        public string Ubicacion { get; set; }
        public string MedidaPropiedad { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public string Publicado { get; set; }
        public string Estado { get; set; }
        public string Intencion { get; set; }
        public string Topografia { get; set; }
        public string Moneda { get; set; }
        public string BarrioPoblado { get; set; }
    }
}
