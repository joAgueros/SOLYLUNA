using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.ViewModels.Agendas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class AgendaOficinaController : Controller
    {

        private readonly IContenedorTrabajo contenedorTrabajo;

        public AgendaOficinaController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetCalendarEvents(string start, string end)
        {
            List<Eventos> events = contenedorTrabajo.Events.GetCalendarEvents(start, end);

            return Json(events);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] Eventos evt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modelos.Entidades.Response mensaje = await contenedorTrabajo.Events.AddEvent(evt);

                    if (mensaje.EsCorrecto)
                    {
                        if (mensaje.Mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                        {

                            return Json(new { message = true, data = mensaje.Resultado });
                        }
                        else if (mensaje.Mensaje.Equals("Ya existe")) /*Este permite indicar que el registro con el correo
                                                               indicado ya existe en la base de datos*/
                        {
                            return Json(new { data = mensaje.Mensaje });
                        }
                    }
                    else /*Ocurrio un error al registrar*/
                    {
                        return Json(new { data = "Error" });
                    }

                }

                return View();
            }
            catch (Exception)
            {
                return Json(new { data = "Error" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateEvent([FromBody] Eventos evt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modelos.Entidades.Response mensaje = await contenedorTrabajo.Events.UpdateEvents(evt);

                    if (mensaje.EsCorrecto)
                    {
                        if (mensaje.Mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                        {
                            return Json(new { message = true, data = mensaje.Resultado });
                        }
                        else if (mensaje.Mensaje.Equals("Ya existe")) /*Este permite indicar que el registro con el correo
                                                                           indicado ya existe en la base de datos*/
                        {
                            return Json(new { data = mensaje.Mensaje });
                        }
                    }
                    else /*Ocurrio un error al registrar*/
                    {
                        return Json(new { data = "Error" });
                    }
                }

                return View();
            }
            catch (Exception)
            {
                return Json(new { data = "Error" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent([FromBody] Eventos evt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modelos.Entidades.Response mensaje = await contenedorTrabajo.Events.DeleteEvents(evt.EventId);

                    if (mensaje.EsCorrecto)
                    {
                        if (mensaje.Mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                        {
                            return Json(new { message = true, data = mensaje.Resultado });
                        }
                        else if (mensaje.Mensaje.Equals("Ya existe")) /*Este permite indicar que el registro con el correo
                                                                           indicado ya existe en la base de datos*/
                        {
                            return Json(new { data = mensaje.Mensaje });
                        }
                    }
                    else /*Ocurrio un error al registrar*/
                    {
                        return Json(new { data = "Error" });
                    }
                }

                return View();
            }
            catch (Exception)
            {
                return Json(new { data = "Error" });
            }

        }
    }
}
