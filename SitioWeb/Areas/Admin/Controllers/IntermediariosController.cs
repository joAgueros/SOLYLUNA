using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.Entidades;
using Modelos.ViewModels.Clientes;
using SitioWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class IntermediariosController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public IntermediariosController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        public async Task<IActionResult> Index()
        {
            RegistrarIntermediarioViewModel model = new()
            {
                TipoPersonas = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion(),
                Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias(),
                Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(string.Empty),
                Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(string.Empty),
                TipoIntermediarios = await contenedorTrabajo.ComboBox.ObtenerListaTiposIntermediarios()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarIntermediario(RegistrarIntermediarioViewModel intermediario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modelos.Entidades.Response mensaje = await contenedorTrabajo.Intermediarios.RegistrarIntermediario(intermediario);

                    if (mensaje.EsCorrecto)
                    {
                        if (mensaje.Mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                        {
                            return Json(new { data = mensaje.Mensaje });
                        }
                        else if (mensaje.Mensaje.Equals("Ya existe")) /*Este permite indicar que el registro con el correo
                                                               indicado ya existe en la base de datos*/
                        {
                            return Json(new { data = mensaje.Mensaje });
                        }
                    }
                    else /*Ocurrio un error al registrar*/
                    {
                        return Json(new { data = mensaje.Mensaje });
                    }

                }

                return View(intermediario);
            }
            catch (Exception)
            {
                return Json(new { data = "Error" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProvinciasCombo()
        {
            IEnumerable<SelectListItem> provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            return Json(new { data = provincias });
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerCantonesCombo(string provincia)
        {
            IEnumerable<SelectListItem> cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(provincia);
            return Json(new { data = cantones });
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerDistritosCombo(string canton)
        {
            IEnumerable<SelectListItem> distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(canton);
            return Json(new { data = distritos });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTiposIdentificacionCombo()
        {
            List<SelectListItem> tiposPersona = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion();
            return Json(new { data = tiposPersona });
        }

        [HttpGet]
        public ActionResult ObtenerListaIntermediarios()
        {
            List<IntermediarioTabla> listaIntermediarios = contenedorTrabajo.Intermediarios.ObtenerListaIntermediarios();
            return Json(new { data = listaIntermediarios });
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("IntermediarioNotFound");
            }

            EditarIntermediarioViewModel clienteObtenido = await contenedorTrabajo.Intermediarios.ObtenerIntermediarioParaEditar(id.Value);

            if (clienteObtenido == null)
            {
                return new NotFoundViewResult("IntermediarioNotFound");
            }

            clienteObtenido.TipoPersonas = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion();
            clienteObtenido.Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            clienteObtenido.TipoIntermediarios = await contenedorTrabajo.ComboBox.ObtenerListaTiposIntermediarios();
            clienteObtenido.Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(clienteObtenido.NombreProvicia.Trim());
            clienteObtenido.Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(clienteObtenido.NombreCanton.Trim());

            return View(clienteObtenido);
        }

        [HttpPost]
        public async Task<IActionResult> EditarRegistroIntermediario(EditarIntermediarioViewModel intermediario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modelos.Entidades.Response mensaje = await contenedorTrabajo.Intermediarios.EditarRegistroIntermediario(intermediario);

                    if (mensaje.EsCorrecto)
                    {
                        if (mensaje.Mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                        {
                            return Json(new { data = mensaje.Mensaje });
                        }
                        else if (mensaje.Mensaje.Equals("Ya existe")) /*Este permite indicar que el registro con el correo
                                                               indicado ya existe en la base de datos*/
                        {
                            return Json(new { data = mensaje.Mensaje });
                        }
                    }
                    else /*Ocurrio un error al registrar*/
                    {
                        return Json(new { data = mensaje.Mensaje });
                    }

                }

                return View(intermediario);
            }
            catch (Exception)
            {
                return Json(new { data = "Error" });
            }

        }
        [HttpGet]
        public async Task<IActionResult> VerRegistroIntermediario(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("IntermediarioNotFound");
            }

            IntermediarioViewModel modelIntermediario = contenedorTrabajo.Intermediarios.ObtenerIntermediario(id.Value);

            if (modelIntermediario == null)
            {
                return new NotFoundViewResult("IntermediarioNotFound");
            }

            modelIntermediario.TipoPersonas = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion();
            modelIntermediario.Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            modelIntermediario.Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(string.Empty);
            modelIntermediario.Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(string.Empty);

            List<MostrarPropiedadTabla> listadoPropiedadesIntermdiarios = contenedorTrabajo.Intermediarios.ObtenerListaPropiedadesPorIntermediario(id.Value);

            return View(new IntermediarioPropiedadViewModel()
            {
                Intermediario = modelIntermediario,
                Propiedades = listadoPropiedadesIntermdiarios
            });
        }

        [HttpPost]
        public async Task<IActionResult> AgregarIntermediarioPropiedad(RegistrarIntermediarioViewModel intermediario)
        {
            Modelos.Entidades.Response mensaje = await contenedorTrabajo.Intermediarios.AgregarIntermediarioPropiedad(intermediario);

            if (mensaje.EsCorrecto)
            {
                if (mensaje.Mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                {
                    return Json(new { success = mensaje.EsCorrecto });
                }
            }

            if (mensaje.Mensaje.Equals("Ya existe"))
            {
                return Json(new { success = mensaje.EsCorrecto, message = mensaje.Mensaje });
            }

            return Json(new { success = mensaje.EsCorrecto });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarIntermediarioPropiedad(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Modelos.Entidades.Response respuesta = await contenedorTrabajo.Intermediarios.EliminarIntermediarioPropiedad(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar eliminar el registro : {respuesta.Mensaje}" });
            }

        }

    }
}

