using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Modelos.ViewModels.Clientes;
using Modelos.ViewModels.Propiedades;
using SitioWeb.Utilidades;
using System;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Cliente.Controllers
{

    [Area("Cliente")]
    public class VendedorWebController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        private readonly IMailHelper mailHelper;

        public VendedorWebController(IContenedorTrabajo contenedorTrabajo, IMailHelper mailHelper)
        {
            this.contenedorTrabajo = contenedorTrabajo;
            this.mailHelper = mailHelper;
        }

        public async Task<IActionResult> Index()
        {
            RegistrarVendedorViewModel model = new()
            {
                TipoPersonas = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion(),
                Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias(),
                Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(string.Empty),
                Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(string.Empty)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVendedor(RegistrarVendedorViewModel vendedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modelos.Entidades.Response mensaje = await contenedorTrabajo.Vendedores.RegistrarVendedor(vendedor, User.Identity.Name);

                    if (mensaje.EsCorrecto)
                    {
                        if (mensaje.Mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                        {

                            string mailcontact = vendedor.CorreoElectronico;
                            string linkPro = "http://realestatesolyluna.com/";

                            bool response = mailHelper.SendMail(mailcontact, "oficina@realestatesolyluna.com", "Registro Vendedor",
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
                                    $"            <h1 style = 'color: #22b8e6; margin: 0 0 7px' >" + vendedor.Nombre + " has sido registrado como un vendedor</h1>" +
                                    $"                    <p style = 'margin: 2px; font-size: 15px'>" +
                                    $"                      Nuestros colaboredores se estarán contactando con su persona para continuar el proceso de registro del Inmueble que desea vender por medio de nosotros <br>" +
                                    $"                      Nuestro horario de estención es de lunes a viernes de 8:00 am 5:00 pm y Sábados de 8:00 am a 12 MD<br>" +
                                    $"         <h3 style = 'color: #044a80; margin: 0 0 7px' > Solo para confirmar que estos sean tus datos personales, sino son, responde al correo indicando los correctos </h3> " +
                                    $"               <ul style = 'font-size: 15px;  margin: 10px 0'>" +
                                    $"        <li>Nombre : " + vendedor.Nombre + " " + vendedor.Apellido1 + " " + vendedor.Apellido2 + "</li>" +
                                    $"        <li> Teléfono Principal: " + vendedor.TelefonoMovil + "</li>" +
                                    $"        <li> Teléfono Oficina: " + vendedor.TelefonoOficina + "</li>" +
                                    $"        <li> Teléfono Casa: " + vendedor.TelefonoCasa + "</li>" +
                                    $"        <li> Correo: " + vendedor.CorreoElectronico + "</li>" +
                                    $"        <li> Dirección Exacta: " + vendedor.DireccionExacta + ".</li>" +
                                    //$"        <li> Gestión de bonos de vivienda.</li>" +
                                    $"      </ul>" +
                                    $"                      Además, te comentamos algunos servicios que puedes encontrar con nosotros</p>" +
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

                            contenedorTrabajo.Save();
                            return Json(new { data = mensaje.Mensaje });
                        }
                        else if (mensaje.Mensaje.Equals("Ya existe")) /*Este permite indicar que el registro con el correo
                                                               indicado ya existe en la base de datos*/
                        {
                            return Json(new { data = mensaje.Mensaje });
                        }
                        else if (mensaje.Mensaje.Equals("Existe"))
                        {
                            return Json(new { data = mensaje.Mensaje });
                        }
                    }
                    else /*Ocurrio un error al registrar*/
                    {
                        return Json(new { data = mensaje.Mensaje });
                    }

                }

                return View(vendedor);
            }
            catch (Exception)
            {
                return Json(new { data = "Error" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProvinciasCombo()
        {
            System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            return Json(new { data = provincias });
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerCantonesCombo(string provincia)
        {
            System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(provincia);
            return Json(new { data = cantones });
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerDistritosCombo(string canton)
        {
            System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(canton);
            return Json(new { data = distritos });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTiposIdentificacionCombo()
        {
            System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> tiposPersona = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion();
            return Json(new { data = tiposPersona });
        }

        [HttpGet]
        public ActionResult ObtenerListaVendedores()
        {
            System.Collections.Generic.List<Modelos.Entidades.VendedorTabla> listadoVendedores = contenedorTrabajo.Vendedores.ObtenerListaVendedores();
            return Json(new { data = listadoVendedores });
        }

        /*ESTE ULTIMO NO COPIARLO PARA LO DE REGISTRO DE COMPRADORES*/
        [HttpGet]
        public async Task<IActionResult> VerRegistroVendedor(int? id)
        {
            if (id == null)
            {
                return View();
            }

            RegistrarPropiedadViewModel modelRegistrarPropiedad = new()
            {
                IngresoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposIngresoPropiedad(),
                UsoSuelo = await contenedorTrabajo.ComboBox.ObtenerTiposUsoSueloPropiedad(),
                EstadoAccesoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposEstadoIngresoPropiedad(),
                TipoPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposPropiedad(),
                UsoPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposUsoPropiedad(),
                MedidasPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposMedidas(),
                TipoPozoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposPozoPropiedad(),
                EstatusPozoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposEstadoPozoPropiedad(),
                Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias(),
                Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(string.Empty),
                Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(string.Empty)
            };

            VendedorViewModel modelVendedor = contenedorTrabajo.Vendedores.ObtenerVendedor(id.Value);
            modelVendedor.TipoPersonas = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion();
            modelVendedor.Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            modelVendedor.Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(string.Empty);
            modelVendedor.Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(string.Empty);

            System.Collections.Generic.List<Modelos.Entidades.MostrarPropiedadTabla> listadoPropiedadesVendedor = contenedorTrabajo.Vendedores.ObtenerListaPropiedadesPorClienteVendedor(id.Value);

            return View(new VendedorPropiedadViewModel()
            {
                Propiedad = modelRegistrarPropiedad,
                Vendedor = modelVendedor,
                Propiedades = listadoPropiedadesVendedor
            });
        }

    }
}
