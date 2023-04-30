using Modelos.Entidades;
using Modelos.ViewModels.Propiedades;
using System.Collections.Generic;

namespace Modelos.ViewModels.FrontEnd.Home
{
    public class HomeViewModel
    {
        public List<VerPropiedadViewModel> DatosPropiedad { get; set; }
        public TotalPorProvincia TotalPorProvincia { get; set; }
    }
}
