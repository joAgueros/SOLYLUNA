using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Modelos.ViewModels.Clientes;
using SitioWeb.Utilidades;
using System;
using System.Threading.Tasks;


namespace SitioWeb.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class ContactoController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        private readonly IMailHelper mailHelper;

        public ContactoController(IContenedorTrabajo contenedorTrabajo, IMailHelper mailHelper)
        {
            this.contenedorTrabajo = contenedorTrabajo;
            this.mailHelper = mailHelper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarContacto(RegistrarContactoViewModel contacto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string mensaje = await contenedorTrabajo.Contacto.RegistrarContacto(contacto);

                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        if (mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                        {

                            string mailcontact = contacto.Correo;
                            string linkPro = "http://realestatesolyluna.com/";

                            bool response = mailHelper.SendMail(mailcontact, "oficina@realestatesolyluna.com", "Contacto",
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
                                    $"            <h1 style = 'color: #22b8e6; margin: 0 0 7px' >" + contacto.Nombre + " pronto te estaremos contactando</h1>" +
                                    $"                    <p style = 'margin: 2px; font-size: 15px'>" +
                                    $"                      Nuestro horario de estención es de lunes a viernes de 8:00 am 5:00 pm y Sábados de 8:00 am a 12 MD <br>" +
                                    $"                      Estamos cada día mejorando nuestro servicio, por ello, si tiene algun comentario no dude en hacernoslo llegar<br>" +
                                    $"         <h3 style = 'color: #044a80; margin: 0 0 7px' > Solo para confirmar que estos sean tus datos personales, sino son, responde al correo indicando los correctos </h3> " +
                                    $"               <ul style = 'font-size: 15px;  margin: 10px 0'>" +
                                    $"        <li>Nombre : " + contacto.Nombre + " " + contacto.Apellidos + "</li>" +
                                    $"        <li> Teléfono: " + contacto.Tel + "</li>" +
                                    $"        <li> Correo: " + contacto.Correo + "</li>" +
                                    $"        <li> Mensaje: " + contacto.Descripcion + ".</li>" +
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
                            if (response)
                            {

                            }

                            contenedorTrabajo.Save();
                            return Json(new { data = mensaje });
                        }
                        else if (mensaje.Equals("Ya existe")) /*Este permite indicar que el registro con el correo
                                                               indicado ya existe en la base de datos*/
                        {
                            return Json(new { data = mensaje });
                        }
                    }
                    else /*Ocurrio un error al registrar*/
                    {
                        return Json(new { data = mensaje });
                    }

                }

                return View(contacto);
            }
            catch (Exception)
            {
                return Json(new { data = "Error" });
            }

        }

    }
}
