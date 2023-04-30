using Microsoft.AspNetCore.Http;
using Modelos.Entidades;
using System.Collections.Generic;

namespace Modelos.ViewModels.Propiedades
{
    public class GaleriaViewModel
    {
        public int IdPropiedad { get; set; }
        public int IdConstruccion { get; set; }
        public IFormFile UrlImagen { get; set; }
        public List<Imagen> Imagenes { get; set; }
    }
}
