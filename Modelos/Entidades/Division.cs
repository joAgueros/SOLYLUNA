using System.Collections.Generic;

namespace Modelos.Entidades
{
    public class Division
    {
        public int IdDivision { get; set; }
        public int IdConstruccion { get; set; }
        public string NombreDescriptivo { get; set; }
        public string Descripcion { get; set; }
        public int IdConstruccionDivision { get; set; }
        public List<Material> Materiales { get; set; }
    }

    public class  Material
    {
        public int IdMaterial { get; set; }
        public string Descripcion { get; set; }
        public int IdConstruccionDivisiones { get; set; }
        public int IdDivision { get; set; }
    }
}
