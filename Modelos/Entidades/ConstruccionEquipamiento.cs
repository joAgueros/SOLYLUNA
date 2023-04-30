namespace Modelos.Entidades
{
    public class ConstruccionEquipamiento 
    {
        public int IdEquipamiento { get; set; }
        public int IdConstruccion { get; set; }
        public int IdConstruccionEquipamiento { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion  { get; set; }
    }
}
