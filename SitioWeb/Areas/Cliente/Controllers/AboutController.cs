using Microsoft.AspNetCore.Mvc;

namespace SitioWeb.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
