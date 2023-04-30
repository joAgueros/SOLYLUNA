using Modelos.Entidades;
using Modelos.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.ViewModels.Alquileres
{
    public class AlquileresViewModel
    {
        public List<VerPropiedadViewModel> Propiedades { get; set; }
        public List<MostrarPropiedadTabla> ListaPropiedades { get; set; }
        
    }
}
