using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Modelos.ViewModels.Alquileres;
using SitioWeb.Utilidades;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Cliente.Controllers
{
    [Area("Cliente")]

    public class AlquilerController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;


        public AlquilerController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;

        }

        // GET: AlquilerController
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult(string.Empty);
            }
            AlquileresViewModel datosAlquiler = new()
            {
                Propiedades = await contenedorTrabajo.Alquiler.ObtenerDatosPropiedadesAlquiler(id.Value)
            };
            return View(datosAlquiler);
        }
    }
}
