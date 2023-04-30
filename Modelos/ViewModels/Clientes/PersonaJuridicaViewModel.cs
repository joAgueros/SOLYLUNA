using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.ViewModels.Clientes
{
    public class PersonaJuridicaViewModel
    {
        public int IdPersonaJuridica { get; set; }
        public string nombreEntidad { get; set; }
        public string RazonSocial { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
    }
}
