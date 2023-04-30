namespace Modelos.Entidades
{
    public class ConstruccionCaracteristica
    {
        public int IdCaracteristica { get; set; }
        public int IdConstruccion { get; set; }
        public int IdConstruccionCaracteristica { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
    }
}
