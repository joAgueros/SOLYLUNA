using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.Entidades;
using Modelos.ViewModels.Propiedades;
using System.Collections.Generic;

namespace Modelos.ViewModels.FrontEnd.Propiedades
{
    public class PropiedadesBusquedaViewModel
    {
        public List<VerPropiedadViewModel> Propiedades { get; set; }
        public List<MostrarPropiedadTabla> ListaPropiedades { get; set; }
        public List<VerPropiedadViewModel> DatosPropiedad { get; set; }
        public IEnumerable<SelectListItem> TiposMoneda { get; set; }
        public IEnumerable<SelectListItem> ValoresMinimos { get; set; }
        public IEnumerable<SelectListItem> ValoresMaximos { get; set; }
        public int TipoMonedaId { get; set; }
    }
}
