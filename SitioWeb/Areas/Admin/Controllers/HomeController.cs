using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.ViewModels.HomeAdmin;

namespace SitioWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            AdminViewModel datosIndex = new()
            {
                TotalesPanelAdmin = contenedorTrabajo.HomeAdmin.Totales()
            };

            return View(datosIndex);
        }
    }
}
