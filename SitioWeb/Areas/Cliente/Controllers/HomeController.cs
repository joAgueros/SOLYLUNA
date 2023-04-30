using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Data.SqlClient;
using Modelos.ViewModels.FrontEnd.Home;
using Modelos.ViewModels.Propiedades;
using SitioWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        private readonly IHtmlLocalizer<HomeController> htmlLocalizer;
        private readonly IMailHelper mailHelper;

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

            return LocalRedirect(returnUrl);
        }

        public HomeController(IContenedorTrabajo contenedorTrabajo, IHtmlLocalizer<HomeController> htmlLocalizer, IMailHelper mailHelper)
        {
            this.contenedorTrabajo = contenedorTrabajo;
            this.htmlLocalizer = htmlLocalizer;
            this.mailHelper = mailHelper;
        }

        public async Task<IActionResult> ConfirmarCorreo(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            Modelos.Entidades.ApplicationUser user = await contenedorTrabajo.Usuario.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await contenedorTrabajo.Usuario.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }

        public async Task<IActionResult> Index()
        {
            LocalizedHtmlString test = htmlLocalizer["HelloWorld"];
            ViewData["HelloWorld"] = test;
            HomeViewModel datosIndex = new()
            {
                DatosPropiedad = await contenedorTrabajo.HomeCliente.ObtenerDatosPropiedadesRecientes(string.Empty, "Proc2"),
                TotalPorProvincia = contenedorTrabajo.HomeCliente.TotalPorProvincia()
            };

            return View(datosIndex);
        }

        public IActionResult Recuperacion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataBusquedaVenta(string busqueda, string intencion)
        {
             List<string> listadoBusqueda = contenedorTrabajo.HomeCliente.ObtenerFiltroBusqueda(busqueda, intencion);

            if (listadoBusqueda != null)
            {
                return Json(new { data = listadoBusqueda });
            }
            else
            {
                return Json(new { data = "ERROR" });
            }
        }

        [HttpPost]
        public IActionResult GetDataBusquedaRenta(string busqueda, string intencion)
        {
            List<string> listadoBusqueda = contenedorTrabajo.HomeCliente.ObtenerFiltroBusqueda(busqueda, intencion);

            if (listadoBusqueda != null)
            {
                return Json(new { data = listadoBusqueda });
            }
            else
            {
                return Json(new { data = "ERROR" });
            }
        }

        public async Task<IActionResult> BusquedaPropiedades(string buscar, string intencion, string seleccionado)
        {
            try
            {
                if (string.IsNullOrEmpty(buscar) && !seleccionado.Equals("01")) /*cuando no se digita nada en la caja de busqueda y se elige cualquier opcion*/
                {
                    List<VerPropiedadViewModel> listadoPropiedades1 = await contenedorTrabajo.HomeCliente.
                   ObtenerPropiedadesBusquedaLugarIntencion(string.Empty, intencion, seleccionado, "Proc4");

                    if (listadoPropiedades1.Count > 0)
                    {
                        listadoPropiedades1[0].VanTodas = true;
                    }
                    if (listadoPropiedades1.Count == 0)
                    {
                        return View(new List<VerPropiedadViewModel>() { new VerPropiedadViewModel { SinResultados = true, VanTodas = false } });
                    }

                    return View(listadoPropiedades1);
                }

                if (string.IsNullOrEmpty(buscar) && seleccionado.Equals("01")) /*cuando no se digita nada en la caja de busqueda y se elige la opcion TODAS*/
                {
                    List<VerPropiedadViewModel> listadoGeneral = await contenedorTrabajo.HomeCliente.ObtenerDatosPropiedadesRecientes(intencion, "Proc1");

                    if (listadoGeneral.Count > 0)
                    {
                        listadoGeneral[0].VanTodas = true;
                    }
                    if (listadoGeneral.Count == 0)
                    {
                        return View(new List<VerPropiedadViewModel>() { new VerPropiedadViewModel { SinResultados = true, VanTodas = false } });
                    }

                    return View(listadoGeneral);
                }

                if (!string.IsNullOrEmpty(buscar) && !seleccionado.Equals("01")) /*cuando se digita algo en la caja de busqueda y se elige un tipo de uso de propiedad*/
                {
                    List<VerPropiedadViewModel> listadoPropiedades1 = await contenedorTrabajo.HomeCliente.
                   ObtenerPropiedadesBusquedaLugarIntencion(buscar, intencion, seleccionado, "Proc1");

                    if (listadoPropiedades1.Count > 0)
                    {
                        listadoPropiedades1[0].VanTodas = true;
                    }
                    if (listadoPropiedades1.Count == 0)
                    {
                        return View(new List<VerPropiedadViewModel>() { new VerPropiedadViewModel { SinResultados = true, VanTodas = false } });
                    }

                    return View(listadoPropiedades1);
                }

                /*Aca llega cuando se digita algo en la caja de busqueda y se elige la opcion TODAS*/
                List<VerPropiedadViewModel> listadoPropiedades = await contenedorTrabajo.HomeCliente.
                    ObtenerPropiedadesBusquedaLugarIntencion(buscar, intencion, seleccionado, "Proc3");

                if (listadoPropiedades == null)
                {
                    return View(new List<VerPropiedadViewModel>() { new VerPropiedadViewModel { SinResultados = true, VanTodas = false } });
                }
                if (listadoPropiedades.Count == 0)
                {
                    return View(new List<VerPropiedadViewModel>() { new VerPropiedadViewModel { SinResultados = true, VanTodas = false } });
                }

                return View(listadoPropiedades);
            }
            catch (Exception)
            {
                return View(new List<VerPropiedadViewModel>() { new VerPropiedadViewModel { SinResultados = true, VanTodas = false } });
            }
        }

        public async Task<IActionResult> BusquedaPropiedadesPorProvincia(string provincia)
        {
            if (string.IsNullOrEmpty(provincia))
            {
                return Json(new { success = false, Message = "Debe seleccionar un lugar para proceder con la búsqueda" });
            }

            List<VerPropiedadViewModel> listadoPropiedades = await contenedorTrabajo.HomeCliente.
                ObtenerPropiedadesBusquedaLugarIntencion(provincia, string.Empty, string.Empty, "Proc2");

            if (listadoPropiedades.Count == 0)
            {
                return Json(new { success = false, Message = "No hay resultados" });
            }

            return View(listadoPropiedades);
        }

        [HttpGet]
        public async Task<IActionResult> VerInformacionPropiedad(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult(string.Empty);
            }

            InformacionPropiedadViewModel obtenerPropiedad = await contenedorTrabajo.HomeCliente.ObtenerInformacionPropiedad(id.Value);

            if (obtenerPropiedad == null)
            {
                //return new NotFoundViewResult(string.Empty);
                return RedirectToAction("Cliente", "Home", "NoExiste");
            }

            return View(obtenerPropiedad);
        }

        [HttpGet]
        public async Task<IActionResult> VerInformacionConstruccion(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult(string.Empty);
            }

            InformacionPropiedadViewModel obtenerPropiedad = await contenedorTrabajo.HomeCliente.ObtenerInformacionPropiedad(id.Value);


            if (obtenerPropiedad == null)
            {
                return new NotFoundViewResult(string.Empty);
            }
            return Json(new { data = obtenerPropiedad });
        }

        [HttpPost]
        public ActionResult EnviarSolicitud(string nombre, string correo, string telefono, string comentario, string idProp)
        {
            try
            {

                string mensaje = "OK";
                string mailcontact = correo;

                string linkPro = "http://realestatesolyluna.com/";

                bool response = mailHelper.SendMail(mailcontact, "oficina@realestatesolyluna.com", "Solicitud Información",
                        $"<table style = 'max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;'>" +
                        $"  <tr>" +
                        $"    <td style = 'background-color: #34495e; text-align: center; padding: 0'>" +

                        $"        <h2>Real Estate Sol Y luna<h2>" +
                        $"       </a>" +
                        $"  </td>" +
                        $"  </tr>" +
                        $"  <tr>" +
                        $"  <td style = 'padding: 0'>" +
                        $"     <img style = 'padding: 0; display: block' src = 'https://descubremexico.com/wp-content/uploads/2019/06/como-vender-tu-casa-bienes-raices.jpg' width = '100%'>" +
                        $"  </td>" +
                        $"</tr>" +
                        $"<tr>" +
                        $" <td style = 'background-color: #ecf0f1'>" +
                        $"      <div style = 'color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'>" +
                        $"            <h1 style = 'color: #22b8e6; margin: 0 0 7px' > Estimado/a " + nombre + " pronto te estaremos contactando</h1>" +
                        $"                    <p style = 'margin: 2px; font-size: 15px'>" +
                        $"                      Nuestro horario de estención es de lunes a viernes de 8:00 am 5:00 pm y Sábados de 8:00 am a 12 MD <br>" +
                        $"                      Estamos cada día mejorando nuestro servicio, por ello, si tiene algun comentario no dude en hacernoslo llegar<br>" +
                        $"         <h3 style = 'color: #044a80; margin: 0 0 7px' > En estos momentos tienes interes en esta propiedad " +
                        $"               <ul style = 'font-size: 15px;  margin: 10px 0'>" +
                        $"                       <li>Codigo de Propiedad : " + idProp + "</li>" +
                        $"                </ul>" +
                        $"               <h3 style = 'color: #044a80; margin: 0 0 7px' >La empresa se contactará contigo por medio de los datos brindados " +
                        $"               <ul style = 'font-size: 15px;  margin: 10px 0'>" +
                        $"                       <li>Nombre : " + nombre + "</li>" +
                        $"                       <li> Correo: " + correo + " </li>" +
                        $"                       <li> Telefono: " + telefono + "</li>" +
                        $"                       <li> Dirección exacta: " + comentario + ".</li>" +
                        $"                </ul>" +
                        $"        Además, te comentamos algunos servicios que puedes encontrar con nosotros</p>" +
                        $"      <ul style = 'font-size: 15px;  margin: 10px 0'>" +
                        $"        <li> Venta de Bienes inmuebles.</li>" +
                        $"        <li> Alquiler de casas.</li>" +
                        $"        <li> Compra de lote o propiedad.</li>" +
                        $"        <li> Gestión de bonos de vivienda.</li>" +
                        $"      </ul>" +
                        //$"  <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                        //$"    <img style = 'padding: 0; width: 200px; margin: 5px' src = 'https://veterinarianuske.com/wp-content/uploads/2018/07/tarjetas.png'>" +
                        //$"  </div>" +
                        $"  <div style = 'width: 100%; text-align: center'>" +
                        $"    <h2 style = 'color: #e67e22; margin: 5 5 7px' >Nuevas Propiedades</h2>" +
                        $"    <p style = 'font-size: 16px; text-align: center;margin: 10px 10px 0' Para ver nuestras propiedades nuevas puedes ingresar a este link p> </br> " +
                        $"    <a style ='text-decoration: none; border-radius: 5px; margin-top:5px; padding: 5px 15px; color: white; background-color: #3498db' href = '" + linkPro + "'>Propiedades en venta</a>" +
                        $"    <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 20px 0 0' > Real Estate Sol Y Luna </p>" +
                        $"  </div>" +
                        $" </td >" +
                        $"</tr>" +
                        $"</table>");

                return Json(new { data = mensaje });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

            }

            return Json(new { data = "OK" });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RequiereLogin()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NoExiste()
        {
            return View();
        }

    }
}