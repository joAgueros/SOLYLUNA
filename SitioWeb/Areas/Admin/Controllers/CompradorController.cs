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
    public class CompradorController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        private static int idComprador;

        public CompradorController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        public async Task<IActionResult> IndexAsync()
        {
            RegistrarCompradorViewModel model = new()
            {
                TipoPersonas = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion(),
                Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias(),
                Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(string.Empty),
                Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(string.Empty)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CargarComboTiposDocumentos()
        {
            List<SelectListItem> documentos = await contenedorTrabajo.ComboBox.ObtenerTipoDocumentosPropiedad();
            return Json(new { data = documentos });
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarComprador(RegistrarCompradorViewModel comprador)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Response mensaje = await contenedorTrabajo.Compradores.RegistrarComprador(comprador, User.Identity.Name);

                    if (mensaje.EsCorrecto)
                    {
                        if (mensaje.Mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                        {
                            return Json(new { success = true, data = mensaje.Mensaje });
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

                return View(comprador);
            }
            catch (Exception)
            {
                return Json(new { data = "Error" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VendedorNotFound");
            }

            EditarCompradorViewModel clienteObtenido = await contenedorTrabajo.Compradores.ObtenerCompradorParaEditar(id.Value);
            clienteObtenido.TipoPersonas = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion();
            clienteObtenido.Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            clienteObtenido.Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(clienteObtenido.NombreProvicia.Trim());
            clienteObtenido.Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(clienteObtenido.NombreCanton.Trim());

            if (clienteObtenido == null)
            {
                return new NotFoundViewResult("VendedorNotFound");
            }

            return View(clienteObtenido);
        }

        [HttpGet]
        public IActionResult VerRegistroComprador(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("CompradorNotFound");
            }

            idComprador = id.Value;

            CompradorViewModel modelComprador = contenedorTrabajo.Compradores.ObtenerComprador(id.Value);

            if (modelComprador == null)
            {
                return new NotFoundViewResult("CompradorNotFound");
            }

            List<ReferenciasViewModel> listaReferencias = contenedorTrabajo.Compradores.ObtenerReferenciasComprador(id.Value);

            return View(new CompradorPropiedadViewModel()
            {
                Comprador = modelComprador,
                Referencias = listaReferencias ?? new List<ReferenciasViewModel>()
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditarRegistroComprador(EditarCompradorViewModel comprador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response mensaje = await contenedorTrabajo.Compradores.EditarRegistroComprador(comprador, User.Identity.Name);

                    if (mensaje.EsCorrecto)
                    {
                        if (mensaje.Mensaje.Equals("OK")) /*Si fue satisfactorio, envia todo a la base de datos*/
                        {
                            return Json(new { success = mensaje.EsCorrecto, data = mensaje.Mensaje });
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

                return View(comprador);
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
        public ActionResult ObtenerListaCompradores()
        {
            List<CompradorTabla> listadoCompradores = contenedorTrabajo.Compradores.ObtenerListaCompradores();
            return Json(new { data = listadoCompradores });
        }

        [HttpGet]
        public IActionResult ObtenerListaPropiedades()
        {
            List<MostrarPropiedadTabla> listadoPropiedades = contenedorTrabajo.Propiedades.ObtenerListaPropiedades("N");

            return listadoPropiedades != null ? Json(new
            {
                data = listadoPropiedades
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTiposPropiedad()
        {
            List<SelectListItem> tiposPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposPropiedad();
            return Json(new { data = tiposPropiedad });
        }

        public async Task<IActionResult> AgregarDatosCompraPropiedad(CaracteristicasPropiedadElegida caracteristica)
        {

            try
            {
                Response respuesta = await contenedorTrabajo.Compradores.AgregarDatosPropiedadElegida(caracteristica, User.Identity.Name);

                return respuesta.EsCorrecto ? Json(new { data = "OK" }) : Json(new { data = "ERROR" });
            }
            catch (Exception ex)
            {
                return Json(new { data = $"ERROR {ex.Message}" });
            }

        }

        [HttpGet]
        public ActionResult ObtenerCaracteristicasPropiedadObtenida()
        {
            List<CaracteristicasPropiedadElegida> listado = contenedorTrabajo.Compradores.ObtenerCaracteristicasPropiedadObtenida(idComprador);

            return listado != null ? Json(new { data = listado }) : Json(new { data = "ERROR" });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCaracteristicaPropiedadAdquirida(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Compradores.EliminarCaracteristicaPropiedadAdquirida(id.Value, User.Identity.Name);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {
                return respuesta.Mensaje.Equals("No existe") ? Json(new
                {
                    success = false,
                    message = respuesta.Mensaje
                }) : Json(new
                {
                    success = false,
                    message = $"Ha ocurrido un error al intentar eliminar el registro : {respuesta.Mensaje}"
                });
            }

        }

        [HttpPost]
        public IActionResult ObtenerCaracteristicasPropiedadElegida(CaracteristicasPropiedadElegida caracteristica)
        {
            Response respuesta = contenedorTrabajo.Compradores.ObtenerCaracteristicaPropiedadAdquirida(caracteristica.Id);

            return respuesta.EsCorrecto
                ? Json(new { success = true, data = respuesta.Resultado as CaracteristicasPropiedadElegida })
                : respuesta.Mensaje.Equals("No existe") ? Json(new
                {
                    success = false,
                    message = "No existe"
                }) : Json(new
                {
                    success = false,
                    message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}"
                });
        }

        [HttpPost]
        public async Task<IActionResult> EditarCaracteristicasPropiedadElegida(CaracteristicasPropiedadElegida caracteristica)
        {

            Response respuesta = await contenedorTrabajo.Compradores.EditarCaracteristicasPropiedadElegida(caracteristica, User.Identity.Name);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                message = respuesta.Mensaje
            }) : respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = "No existe"
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}"
            });

        }

        public async Task<IActionResult> AgregarDatosGestion(GestionCompra gestion)
        {

            try
            {
                Response respuesta = await contenedorTrabajo.Compradores.AgregarDatosGestion(gestion, User.Identity.Name);

                return respuesta.EsCorrecto ? Json(new
                {
                    data = "OK"
                }) : Json(new
                {
                    data = "ERROR"
                });
            }
            catch (Exception ex)
            {
                return Json(new { data = $"ERROR {ex.Message}" });
            }

        }

        [HttpGet]
        public ActionResult ObtenerGestiones()
        {
            List<GestionCompra> listado = contenedorTrabajo.Compradores.ObtenerGestiones(idComprador);

            return listado != null ? Json(new
            {
                data = listado
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarGestion(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Compradores.EliminarGestion(id.Value, User.Identity.Name);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {
                return respuesta.Mensaje.Equals("No existe") ? Json(new
                {
                    success = false,
                    message = respuesta.Mensaje
                }) : Json(new
                {
                    success = false,
                    message = $"Ha ocurrido un error al intentar eliminar el registro : {respuesta.Mensaje}"
                });
            }

        }

        [HttpPost]
        public IActionResult ObtenerGestion(GestionCompra gestion)
        {
            Response respuesta = contenedorTrabajo.Compradores.ObtenerGestion(gestion.Id);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                data = respuesta.Resultado as GestionCompra
            }) : respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = "No existe"
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditarGestion(GestionCompra gestion)
        {

            Response respuesta = await contenedorTrabajo.Compradores.EditarGestion(gestion, User.Identity.Name);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                message = respuesta.Mensaje
            }) : respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = "No existe"
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}"
            });

        }

        public async Task<IActionResult> AgregarDatosResultadoSugef(ResultadoSugef resultado)
        {

            try
            {
                Response respuesta = await contenedorTrabajo.Compradores.AgregarDatosResultadoSugef(resultado, User.Identity.Name);

                return respuesta.EsCorrecto ? Json(new
                {
                    data = "OK"
                }) : Json(new
                {
                    data = "ERROR"
                });
            }
            catch (Exception ex)
            {
                return Json(new { data = $"ERROR {ex.Message}" });
            }

        }

        [HttpGet]
        public ActionResult ObtenerResultadosSugef()
        {
            List<ResultadoSugef> listado = contenedorTrabajo.Compradores.ObtenerResultadosSugef(idComprador);

            return listado != null ? Json(new
            {
                data = listado
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarResultadosSugef(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Compradores.EliminarResultadosSugef(id.Value, User.Identity.Name);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {
                return respuesta.Mensaje.Equals("No existe") ? Json(new
                {
                    success = false,
                    message = respuesta.Mensaje
                }) : Json(new
                {
                    success = false,
                    message = $"Ha ocurrido un error al intentar eliminar el registro : {respuesta.Mensaje}"
                });
            }

        }

        [HttpPost]
        public IActionResult ObtenerResultadoSugef(ResultadoSugef resultado)
        {
            Response respuesta = contenedorTrabajo.Compradores.ObtenerResultadoSugef(resultado.Id);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                data = respuesta.Resultado as ResultadoSugef
            }) : respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = "No existe"
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditarResultadoSugef(ResultadoSugef resultado)
        {

            Response respuesta = await contenedorTrabajo.Compradores.EditarResultadoSugef(resultado, User.Identity.Name);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                message = respuesta.Mensaje
            }) : respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = "No existe"
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}"
            });

        }

        public async Task<IActionResult> AgregarDatosResultadoSolicitante(ResultadoSolicitante resultado)
        {

            try
            {
                Response respuesta = await contenedorTrabajo.Compradores.AgregarDatosResultadoSolicitante(resultado, User.Identity.Name);

                return respuesta.EsCorrecto ? Json(new
                {
                    data = "OK"
                }) : Json(new
                {
                    data = "ERROR"
                });
            }
            catch (Exception ex)
            {
                return Json(new { data = $"ERROR {ex.Message}" });
            }

        }

        [HttpGet]
        public ActionResult ObtenerResultadosSolicitante()
        {
            List<ResultadoSolicitante> listado = contenedorTrabajo.Compradores.ObtenerResultadosSolicitante(idComprador);

            return listado != null ? Json(new
            {
                data = listado
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarResultadosSolicitante(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Compradores.EliminarResultadosSolicitante(id.Value, User.Identity.Name);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {
                return respuesta.Mensaje.Equals("No existe") ? Json(new
                {
                    success = false,
                    message = respuesta.Mensaje
                }) : Json(new
                {
                    success = false,
                    message = $"Ha ocurrido un error al intentar eliminar el registro : {respuesta.Mensaje}"
                });
            }

        }

        [HttpPost]
        public IActionResult ObtenerResultadoSolicitante(ResultadoSolicitante resultado)
        {
            Response respuesta = contenedorTrabajo.Compradores.ObtenerResultadoSolicitante(resultado.Id);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                data = respuesta.Resultado as ResultadoSolicitante
            }) : respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = "No existe"
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditarResultadoSolicitante(ResultadoSolicitante resultado)
        {

            Response respuesta = await contenedorTrabajo.Compradores.EditarResultadoSolicitante(resultado, User.Identity.Name);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                message = respuesta.Mensaje
            }) : respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = "No existe"
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}"
            });

        }

        /*DOCUMENTOS PROPIEDAD*/

        [HttpGet]
        public ActionResult ObtenerListadoDocumentosAdquiridos()
        {
            List<DocumentoComprador> listadoDocumentos = contenedorTrabajo.Compradores.ObtenerDocumentosComprador(idComprador);

            return listadoDocumentos != null ? Json(new
            {
                data = listadoDocumentos
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpPost]
        public async Task<ActionResult> ObtenerDocumentoComprador(DocumentoComprador documento)
        {
            Response respuesta = await contenedorTrabajo.Compradores.ObtenerDocumentoComprador(documento.IdDocumentoComprador);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                data = respuesta.Resultado as DocumentoComprador
            }) : respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = "No existe"
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}"
            });
        }


        [HttpPost]
        public async Task<IActionResult> AgregarDatosEditadosDocumentosComprador(DocumentoComprador documento)
        {

            Response respuesta = await contenedorTrabajo.Compradores.AgregarDatosEditadosDocumentosComprador(documento, User.Identity.Name);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                message = respuesta.Mensaje
            }) : respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = "No existe"
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}"
            });

        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosDocumentos(DocumentoComprador documento)
        {

            Response respuesta = await contenedorTrabajo.Compradores.AgregarDatosDocumentoComprador(documento, User.Identity.Name);

            return respuesta.EsCorrecto ? Json(new
            {
                success = true,
                message = respuesta.Mensaje
            }) : Json(new
            {
                success = false,
                message = respuesta.Mensaje
            });

        }

        [HttpDelete]
        public async Task<IActionResult> EliminarDocumentoComprador(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Compradores.EliminarDocumentoComprador(id.Value, User.Identity.Name);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {
                return respuesta.Mensaje.Equals("No existe") ? Json(new
                {
                    success = false,
                    message = respuesta.Mensaje
                }) : Json(new
                {
                    success = false,
                    message = $"Ha ocurrido un error al intentar eliminar el registro : {respuesta.Mensaje}"
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstadoReferencia(ReferenciasViewModel referencias)
        {
            if (referencias != null)
            {
                Response respuesta = await contenedorTrabajo.Compradores.CambiarEstadoReferencias(referencias, User.Identity.Name);

                if (respuesta.EsCorrecto)
                {
                    return Json(new { success = respuesta.EsCorrecto, message = respuesta.Mensaje });
                }
                else
                {
                    return Json(new { success = respuesta.EsCorrecto, message = respuesta.Mensaje });
                }
            }

            return Json(new { success = false });

        }

    }
}

