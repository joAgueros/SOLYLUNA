using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.ViewModels.Clientes
{
    public class RegistrarReferenciasViewModel
    {
        public int IdReferencias { get; set; }
        public int IdClienteV { get; set; }
        public string Descripcion { get; set; }
        public char Estado { get; set; }
    }
}