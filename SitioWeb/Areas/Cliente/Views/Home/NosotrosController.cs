using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Cliente.Controllers
{
    public class NosotrosController : Controller
    {
        public IActionResult Nosotros()
        {
            return View();
        }
       
    }
}
