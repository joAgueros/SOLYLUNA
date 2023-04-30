using Modelos.Entidades;
using System.Collections.Generic;

namespace Modelos.ViewModels.FrontEnd.Home
{
    public class InformacionConstruccionViewModel
    {
        public Construccion Construccion { get; set; }
        public List<ConstruccionEquipamiento> Equipamientos { get; set; }
        public List<ConstruccionDivision> Divisiones { get; set; }
        public List<ConstruccionCaracteristica> Caracteristicas { get; set; }
        public List<ConstruccionCableado> Cableados { get; set; }
        public List<Imagen> Imagenes { get; set; }

    }
}
