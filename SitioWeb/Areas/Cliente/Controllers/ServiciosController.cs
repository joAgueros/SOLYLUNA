using Microsoft.AspNetCore.Mvc;

namespace SitioWeb.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class ServiciosController : Controller
    {
        public IActionResult Servicios()
        {
            return View();
        }
    }
}
