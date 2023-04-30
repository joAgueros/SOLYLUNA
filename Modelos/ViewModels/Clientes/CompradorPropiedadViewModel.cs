using Modelos.Entidades;
using Modelos.ViewModels.Propiedades;
using System.Collections.Generic;

namespace Modelos.ViewModels.Clientes
{
    public class CompradorPropiedadViewModel
    {
        public CompradorViewModel Comprador { get; set; }
        public RegistrarPropiedadViewModel Propiedad { get; set; }
        public List<MostrarPropiedadTabla> Propiedades { get; set; }
        public List<ReferenciasViewModel> Referencias { get; set; }
    }
}
