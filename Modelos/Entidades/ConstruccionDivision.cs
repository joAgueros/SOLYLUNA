using System.Collections.Generic;

namespace Modelos.Entidades
{
    public class ConstruccionDivision
    {
        public int IdDivision { get; set; }
        public int IdConstruccion { get; set; }
        public int IdConstruccionDivision { get; set; }
        public string NombreDescriptivo { get; set; }
        public string Observacion { get; set; }
        public string Descripcion { get; set; }
        public List<MaterialesDivision> Materiales { get; set; }
    }

    public class MaterialesDivision
    {
        public int IdDivisionMateriales { get; set; }
        public int IdDivision { get; set; }
        public int IdConstruccionDivision { get; set; }
        public int IdMaterial { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
