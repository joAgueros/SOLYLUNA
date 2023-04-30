using Microsoft.AspNetCore.Mvc.Rendering;

namespace Modelos.Entidades
{
    public class ComboPersonalizado : SelectListItem
    {
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
}
