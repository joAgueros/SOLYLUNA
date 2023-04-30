using Modelos.Entidades;
using Modelos.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.ViewModels.Clientes
{
    public class IntermediarioPropiedadViewModel
    {    public IntermediarioViewModel Intermediario { get; set; }
        //public RegistrarPropiedadInterVeiwModel Propiedad { get; set; }
        public List<MostrarPropiedadTabla> Propiedades { get; set; }
    }
}
