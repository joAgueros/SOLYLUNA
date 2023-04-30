using AccesoDatos.Data.Repository;
using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Modelos.Entidades;
using Modelos.ViewModels.Propiedades;
using SitioWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class PropiedadController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        private readonly IWebHostEnvironment environment;
        private static int idPropiedad = 0;

        public PropiedadController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment environment)
        {
            this.contenedorTrabajo = contenedorTrabajo;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Bitacora()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PropiedadesEliminadas()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CargarListaBitacoraPropiedades()
        {
            List<Bitacora> listadoBitacora = contenedorTrabajo.Propiedades.ListaBitacoraPropiedades();

            return listadoBitacora != null ? Json(new
            {
                data = listadoBitacora
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpPost]
        public IActionResult ObtenerListaBitacoraPropiedades(Bitacora bitacora)
        {
            List<Bitacora> listadoBitacora = contenedorTrabajo.Propiedades.ObtenerListaBitacoraPropiedades(bitacora);

            return listadoBitacora != null ? Json(new
            {
                data = listadoBitacora
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpGet]
        public IActionResult ObtenerListaPropiedades()
        {
            List<MostrarPropiedadTabla> listadoPropiedades = contenedorTrabajo.Propiedades.ObtenerListaPropiedades("N");

            return listadoPropiedades != null ? Json(new { data = listadoPropiedades }) : Json(new { data = "ERROR" });
        }

        [HttpGet]
        public IActionResult ObtenerListaPropiedadesEliminadas()
        {
            List<MostrarPropiedadTabla> listadoPropiedades = contenedorTrabajo.Propiedades.ObtenerListaPropiedades("S");

            return listadoPropiedades != null ? Json(new { data = listadoPropiedades }) : Json(new { data = "ERROR" });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarPropiedad(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.EliminarPropiedad(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {return respuesta.Mensaje.Equals("No existe") ? Json(new
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
        public async Task<IActionResult> ActivarPropiedadEnListaPrincipal(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.ActivarPropiedadEnListaPrincipal(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido activado correctamente" });
            }
            else
            {return respuesta.Mensaje.Equals("No existe") ? Json(new
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

        [HttpGet]
        public async Task<IActionResult> Galeria(int? id)
        {

            if (id == null)
            {
                return new NotFoundViewResult("PropiedadNotFound");
            }

            /*validar que la propiedad exista*/
            AccesoDatos.BlogCore.Models.TbPropiedade existe = contenedorTrabajo.Propiedades.Get(id.Value);

            if (existe == null)
            {
                return new NotFoundViewResult("PropiedadNotFound");
            }

            Response respuesta = await contenedorTrabajo.Propiedades.ObtenerImagenesPropiedad(id.Value);

            if (respuesta.EsCorrecto)
            {
                GaleriaViewModel galeria = new()
                {
                    IdPropiedad = id.Value,
                    Imagenes = (List<Imagen>)respuesta.Resultado
                };

                return View(galeria);
            }

            return new NotFoundViewResult("PropiedadNotFound");
        }

        [HttpPost]
        public async Task<IActionResult> CargarImagen(GaleriaViewModel galeria)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = environment.WebRootPath;
                Microsoft.AspNetCore.Http.IFormFileCollection archivos = HttpContext.Request.Form.Files;

                if (archivos.Count == 0)
                {
                    return View(galeria);
                }

                string nombreCarpeta = $@"{rutaPrincipal}\imagenes\propiedades";
                string pathString = Path.Combine(nombreCarpeta, galeria.IdPropiedad.ToString());

                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }

                //Si es nueva imagen
                string nombreArchivo = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(archivos[0].FileName);
                //Metodo para guardar localmente la imagen pesada
                using (FileStream fileStreams = new(Path.Combine(pathString, $"{nombreArchivo}{extension}"), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                    fileStreams.Flush();
                    fileStreams.Close();
                }

                //Ruta larga con el nombre de la imagen guardada
                string rutaImagenPesada = $@"{rutaPrincipal}\imagenes\propiedades\{galeria.IdPropiedad}\{nombreArchivo}{extension}";
                //Ruta corta de la imagen guardada
                string rutaImagenAux = $@"\imagenes\propiedades\{galeria.IdPropiedad}\{nombreArchivo}{extension}";


                //Nombre único para nombre de imagen redimencionada
                string nombreArchivo2 = Guid.NewGuid().ToString();

                //Ruta Corta que se almacenará en BD
                string rutaImagenBD = $@"\imagenes\propiedades\{galeria.IdPropiedad}\{nombreArchivo2}{extension}";

                //Ruta con nuevo nombre de la imagen
                string rutaImagenResize = $@"{rutaPrincipal}\imagenes\propiedades\{galeria.IdPropiedad}\{nombreArchivo2}{extension}";

                //Metodo para redimencionar imagen
                using (MagickImage oMagickImage = new(rutaImagenPesada))
                {
                    if (oMagickImage.Width > 550)
                    {
                        oMagickImage.Resize(550, 0);
                        oMagickImage.Write(rutaImagenResize);
                    }
                    else
                    {
                        oMagickImage.Write(rutaImagenResize);
                    }

                }

                string rutaImagenEliminar = Path.Combine(rutaPrincipal, rutaImagenAux.TrimStart('\\'));

                if (System.IO.File.Exists(rutaImagenEliminar))
                {
                    System.IO.File.Delete(rutaImagenEliminar);
                }

                Response respuesta = await contenedorTrabajo.Propiedades.AgregarImagenPropiedad(rutaImagenBD, galeria.IdPropiedad);

                if (respuesta.EsCorrecto)
                {
                    Response respuesta2 = await contenedorTrabajo.Propiedades.ObtenerImagenesPropiedad(galeria.IdPropiedad);

                    if (respuesta2.EsCorrecto)
                    {
                        List<Imagen> listadoImagenes = (List<Imagen>)respuesta2.Resultado;
                        return PartialView("~/Areas/Admin/Views/Propiedad/_ListadoImagenes.cshtml", listadoImagenes);
                    }
                    else
                    {
                        return Json(new { success = respuesta2.EsCorrecto });
                    }

                }
                else
                {
                    return Json(new { success = respuesta.EsCorrecto });
                }
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> EditarPropiedad(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("PropiedadNotFound");
            }

            EditarPropiedadViewModel propiedadEditar = await contenedorTrabajo.Propiedades.ObtenerDatosEditarPropiedad(id.Value);

            if (propiedadEditar == null)
            {
                return new NotFoundViewResult("PropiedadNotFound");
            }

            propiedadEditar.IngresoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposIngresoPropiedad();
            propiedadEditar.UsoSuelo = await contenedorTrabajo.ComboBox.ObtenerTiposUsoSueloPropiedad();
            propiedadEditar.EstadoAccesoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposEstadoIngresoPropiedad();
            propiedadEditar.TipoPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposPropiedad();
            propiedadEditar.UsoPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposUsoPropiedad();
            propiedadEditar.MedidasPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposMedidas();
            propiedadEditar.TipoPozoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposPozoPropiedad();
            propiedadEditar.EstatusPozoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposEstadoPozoPropiedad();
            propiedadEditar.NivelCallePropiedad = contenedorTrabajo.ComboBox.ObtenerTiposNivelCalle();
            propiedadEditar.TopografiaPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposTopografia();
            propiedadEditar.Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            propiedadEditar.Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(propiedadEditar.Provincia.Trim());
            propiedadEditar.Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(propiedadEditar.Canton.Trim());

            return View(propiedadEditar);
        }

        [HttpGet]
        public async Task<IActionResult> CargarIntencionElegida()
        {
            Response respuesta = await contenedorTrabajo.Propiedades.ObtenerIntencionPropiedad(idPropiedad);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje, result = respuesta.Resultado });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> CargarVistaElegida()
        {
            Response respuesta = await contenedorTrabajo.Propiedades.ObtenerVistaElegida(idPropiedad);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje, result = respuesta.Resultado });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> ValidarPoseePozoPropiedad()
        {
            Response respuesta = await contenedorTrabajo.Propiedades.ValidarPoseePozoPropiedad(idPropiedad);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje, result = respuesta.Resultado });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProvinciasCombo()
        {
            IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            return Json(new { data = provincias });
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerCantonesCombo(string provincia)
        {
            IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(provincia);
            return Json(new { data = cantones });
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerDistritosCombo(string canton)
        {
            IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(canton);
            return Json(new { data = distritos });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTopografiasCombo()
        {
            IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            return Json(new { data = provincias });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerNivelesCalleCombo()
        {
            IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            return Json(new { data = provincias });
        }

        [HttpGet]
        public async Task<IActionResult> CargarComboTiposServiciosPublicos()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> serviciosPublicos = await contenedorTrabajo.ComboBox.ObtenerListaServiciosPublicos();
            return Json(new { data = serviciosPublicos });
        }

        [HttpGet]
        public async Task<IActionResult> CargarComboTipoPropiedad()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> tipoPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposPropiedad();
            return Json(new { data = tipoPropiedad });
        }

        [HttpGet]
        public async Task<IActionResult> CargarComboTipoUsoSueloPropiedad()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> tipoUsoSueloPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposUsoSueloPropiedad();
            return Json(new { data = tipoUsoSueloPropiedad });
        }

        [HttpGet]
        public async Task<IActionResult> CargarComboUsoPropiedad()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> UsoPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposUsoPropiedad();
            return Json(new { data = UsoPropiedad });
        }

        [HttpGet]
        public async Task<IActionResult> CargarComboAccesibilidades()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> accesbilidades = await contenedorTrabajo.ComboBox.ObtenerListaAccesibilidades();
            return Json(new { data = accesbilidades });
        }

        [HttpGet]
        public IActionResult CargarComboIngresoPropiedad()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ingresoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposIngresoPropiedad();
            return Json(new { data = ingresoPropiedad });
        }

        [HttpGet]
        public IActionResult CargarComboEstadoAccesoPropiedad()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> estadosIngresoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposEstadoIngresoPropiedad();
            return Json(new { data = estadosIngresoPropiedad });
        }

        [HttpGet]
        public async Task<IActionResult> CargarComboMedidaPropiedad()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> tipoMedidasPropiedad = await contenedorTrabajo.ComboBox.ObtenerTiposMedidas();
            return Json(new { data = tipoMedidasPropiedad });
        }

        [HttpGet]
        public IActionResult CargarComboTipoPozoPropiedad()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> tipoPozoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposPozoPropiedad();
            return Json(new { data = tipoPozoPropiedad });
        }

        [HttpGet]
        public IActionResult CargarComboEstatusLegalPozoPropiedad()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> tipoEstatusPozoPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposEstadoPozoPropiedad();
            return Json(new { data = tipoEstatusPozoPropiedad });
        }

        [HttpGet]
        public async Task<IActionResult> CargarComboSituacionesPropiedad()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> tipoSituacionPropiedad = await contenedorTrabajo.ComboBox.ObtenerListaSituacionPropiedad();
            return Json(new { data = tipoSituacionPropiedad });
        }

        [HttpGet]
        public async Task<IActionResult> CargarComboTiposDocumentos()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> documentos = await contenedorTrabajo.ComboBox.ObtenerTipoDocumentosPropiedad();
            return Json(new { data = documentos });
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosInicialesPropiedad(RegistrarPropiedadViewModel registroPropiedad)
        {
            try
            {
                string respuesta = string.Empty;


                respuesta = await contenedorTrabajo.Propiedades.AgregarDatosInicialesPropiedad(registroPropiedad, User.Identity.Name);

                if (respuesta.Equals("OK"))
                {
                    List<MostrarPropiedadTabla> listadoPropiedadesVendedor = contenedorTrabajo.Vendedores.ObtenerListaPropiedadesPorClienteVendedor(registroPropiedad.IdClienteVendedor);

                    return PartialView("~/Areas/Admin/Views/Vendedor/_ListadoPropiedadesCliente.cshtml", listadoPropiedadesVendedor);
                }
                else
                {
                    return Json(new { data = respuesta });
                }

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditarDatosInicialesPropiedad(EditarPropiedadViewModel propiedad)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.EditarDatosInicialesPropiedad(propiedad, User.Identity.Name);

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

        [HttpGet]
        public async Task<IActionResult> VerInformacionPropiedad(int? id)
        {

            if (id == null)
            {
                return new NotFoundViewResult("PropiedadNotFound");
            }

            idPropiedad = id.Value;
            VerPropiedadViewModel propiedad = await contenedorTrabajo.Propiedades.ObtenerDatosPropiedad(idPropiedad);

            if (propiedad == null)
            {
                return new NotFoundViewResult("PropiedadNotFound");
            }

            return View(propiedad);

        }

        [HttpPost]
        public async Task<IActionResult> AgregarNuevaConstruccion(Construccion construccion)
        {

            try
            {
                Response respuesta = await contenedorTrabajo.Propiedades.AgregarNuevaConstruccion(
                                                                        construccion.Descripcion,
                                                                        construccion.IdPropiedad,
                                                                        User.Identity.Name);

                return respuesta.EsCorrecto ? Json(new
                {
                    data = Convert.ToInt32(respuesta.Resultado)
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
        public async Task<ActionResult> ObtenerListaCuotas()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listadoCuotas = await contenedorTrabajo.ComboBox.ObtenerListaCuotas();

            return listadoCuotas != null ? Json(new
            {
                data = listadoCuotas.OrderBy(X => X.Text)
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosAccesibilidades(AccesoPropiedad accesoPropiedad)
        {
            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosAccesoPropiedad(accesoPropiedad);

            return respuesta.EsCorrecto
                ? Json(new { success = true, message = respuesta.Mensaje })
                : Json(new { success = false, message = respuesta.Mensaje });

        }

        [HttpGet]
        public ActionResult ObtenerListaConstrucciones()
        {
            List<Construccion> listadoConstrucciones = contenedorTrabajo.Construcciones.ObtenerListaConstrucciones(idPropiedad);

            return listadoConstrucciones != null ? Json(new
            {
                data = listadoConstrucciones.OrderBy(X => X.Descripcion)
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        /*CARACTERISTICAS PROPIEDAD*/

        [HttpPost]
        public async Task<IActionResult> EliminarCaracteristicaPropiedad(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.EliminarCaracteristicaPropiedad(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {return respuesta.Mensaje.Equals("No existe") ? Json(new
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
        public async Task<IActionResult> DisminuirCaracteristicaPropiedad(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.DisminuirCaracteristicaPropiedad(id.Value);

            if (respuesta.EsCorrecto)
            {
                if (respuesta.Mensaje.Equals("OK"))
                {
                    return Json(new { success = true, message = "OK" });
                }
                else
                {
                    return Json(new { success = true, message = "No rebaja mas" });
                }
            }
            else
            {return respuesta.Mensaje.Equals("No existe") ? Json(new
            {
                success = false,
                message = respuesta.Mensaje
            }) : Json(new
            {
                success = false,
                message = $"Ha ocurrido un error al intentar disminuir el registro : {respuesta.Mensaje}"
            });
            }

        }
        [HttpPost]
        public async Task<IActionResult> AgregarCaracteristicaPropiedad(PropiedadCaracteristica caracteristica)
        {

            try
            {
                bool respuesta = await contenedorTrabajo.Propiedades.AgregarCaracteristicasPropiedad(
                    caracteristica.IdCaracteristica, caracteristica.IdPropiedad);

                if (respuesta)
                {
                    return Json(new { data = "OK" });
                }
                else
                {
                    return Json(new { data = "ERROR" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { data = $"ERROR {ex.Message}" });
            }

        }


        [HttpGet]
        public ActionResult ObtenerListaCaracteristicasPropiedadAdquiridas()
        {
            List<PropiedadCaracteristica> listadoPropiedades = contenedorTrabajo.Propiedades.ObtenerCaracteristicasPropiedadAdquiridas(idPropiedad);

            return listadoPropiedades != null ? Json(new
            {
                data = listadoPropiedades
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListaCaracteristicasPropiedad()
        {
            List<AccesoDatos.BlogCore.Models.TbCaracteristica> listadoCaracteristicas = await contenedorTrabajo.Propiedades.ObtenerTodasLasCaracteristicas();

            return listadoCaracteristicas != null
                ? Json(new { data = listadoCaracteristicas.OrderBy(X => X.Descripcion) })
                : Json(new { data = "ERROR" });
        }

        [HttpPost]
        public async Task<IActionResult> AumentarCaracteristicaPropiedad(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.AumentarCaracteristicasPropiedad(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido aumentado correctamente" });
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
                message = $"Ha ocurrido un error al intentar aumentar la cantidad del registro : {respuesta.Mensaje}"
            });
            }

        }

        /*****************************************************************************************************************/

        /* SERVICIOS MUNICIPALES */

        [HttpGet]
        public ActionResult ObtenerListadoServiciosMunicipalesAdquiridos()
        {
            List<ServicioMunicipal> listadoServicios = contenedorTrabajo.Propiedades.ObtenerServiciosMunicipales(idPropiedad);

            return listadoServicios != null ? Json(new
            {
                data = listadoServicios
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListaServiciosMunicipales()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listadoServiciosMunicipales = await contenedorTrabajo.ComboBox.ObtenerListaServiciosMunicipales();

            return listadoServiciosMunicipales != null ? Json(new
            {
                data = listadoServiciosMunicipales.OrderBy(X => X.Text)
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerServicioMunicipal(ServicioMunicipal servicio)
        {
            Response respuesta = await contenedorTrabajo.Propiedades.ObtenerServicioMunicipal(servicio.IdServicioMunicipal);

            return respuesta.EsCorrecto
                ? Json(new { success = respuesta.EsCorrecto, data = respuesta.Resultado as ServicioMunicipal })
                : respuesta.Mensaje.Equals("No existe")
                    ? Json(new { success = false, message = respuesta.Mensaje })
                    : Json(new { success = false, message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}" });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarTipoServicioMunicipal(int? id)
        {
            if (id == null)
            {
                return Json(new { success = true, message = "OK" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.EliminarTipoServicioMunicipal(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {
                return respuesta.Mensaje.Equals("No existe")
                    ? Json(new { success = false, message = respuesta.Mensaje })
                    : Json(new { success = false, message = $"Ha ocurrido un error al intentar eliminar el registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosEditadosServiciosMunicipales(ServicioMunicipal servicioMunicipal)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosEditadosServiciosMunicipales(servicioMunicipal);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = "No existe" });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosServiciosMunicipales(ServicioMunicipal servicioMunicipal)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosServiciosMunicipales(servicioMunicipal);

            return respuesta.EsCorrecto
                ? Json(new { success = true, message = respuesta.Mensaje })
                : Json(new { success = false, message = respuesta.Mensaje });

        }

        /*****************************************************************************************************************/

        /*RECORRIDO PROPIEDAD*/

        [HttpGet]
        public ActionResult ObtenerListadoRecorridosPropiedadAdquiridos()
        {
            List<AccesoPropiedad> listadoRecorridos = contenedorTrabajo.Propiedades.ObtenerRecorridosPropiedad(idPropiedad);

            return listadoRecorridos != null ? Json(new
            {
                data = listadoRecorridos
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerRecorridoPropiedad(AccesoPropiedad acceso)
        {
            Response respuesta = await contenedorTrabajo.Propiedades.ObtenerRecorridoPropiedad(acceso.IdRecorrido);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, data = respuesta.Resultado as AccesoPropiedad });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = "No existe" });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosEditadosRecorridoPropiedad(AccesoPropiedad accesoPropiedad)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosEditadosRecorridoPropiedad(accesoPropiedad);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = "No existe" });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> EliminarRecorridoPropiedad(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.EliminarRecorridoPropiedad(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {return respuesta.Mensaje.Equals("No existe") ? Json(new
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

        /****************************************************************************************************************/

        /*DOCUMENTOS PROPIEDAD*/

        [HttpGet]
        public ActionResult ObtenerListadoDocumentosAdquiridos()
        {
            List<DocumentoPropiedad> listadoDocumentos = contenedorTrabajo.Propiedades.ObtenerDocumentosPropiedad(idPropiedad);

            return listadoDocumentos != null ? Json(new
            {
                data = listadoDocumentos
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpPost]
        public async Task<ActionResult> ObtenerDocumentoPropiedad(DocumentoPropiedad documento)
        {
            Response respuesta = await contenedorTrabajo.Propiedades.ObtenerDocumentoPropiedad(documento.IdDocumentoPropiedad);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, data = respuesta.Resultado as DocumentoPropiedad });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = "No existe" });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> AgregarDatosEditadosDocumentosPropiedad(DocumentoPropiedad documento)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosEditadosDocumentosPropiedad(documento);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = "No existe" });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosDocumentos(DocumentoPropiedad documento)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosDocumentoPropiedad(documento);

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

        [HttpPost]
        public async Task<IActionResult> EliminarDocumentoPropiedad(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.EliminarDocumentoPropiedad(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {return respuesta.Mensaje.Equals("No existe") ? Json(new
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

        /******************************************************************************************************************************/

        /*SERVICIOS PUBLICOS */

        [HttpPost]
        public async Task<IActionResult> ObtenerServicioPublico(ServicioPublico servicio)
        {
            Response respuesta = await contenedorTrabajo.Propiedades.ObtenerServicioPublico(servicio.IdServicioPublico);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, data = respuesta.Resultado as ServicioPublico });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = "No existe" });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosServiciosPublicos(ServicioPublico servicioPublico)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosServiciosPublicos(servicioPublico);

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

        [HttpPost]
        public async Task<IActionResult> AgregarDatosEditadosServiciosPublicos(ServicioPublico servicioPublico)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosEditadosServiciosPublicos(servicioPublico);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = "No existe" });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}" });
            }

        }

        [HttpGet]
        public ActionResult ObtenerListadoServiciosPublicosAdquiridos()
        {
            List<ServicioPublico> listadoServicios = contenedorTrabajo.Propiedades.ObtenerServiciosPublicos(idPropiedad);

            return listadoServicios != null ? Json(new
            {
                data = listadoServicios
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarTipoServicioPublico(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.EliminarTipoServicioPublico(id.Value);

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

        /****************************************************************************************************************************/

        /*SITUACION PROPIEDAD*/

        [HttpGet]
        public ActionResult ObtenerListadoSituacionLegalPropiedad()
        {
            List<SituacionLegalPropiedad> listadoSituacionLegal = contenedorTrabajo.Propiedades.ObtenerSituacionesPropiedad(idPropiedad);

            return listadoSituacionLegal != null ? Json(new
            {
                data = listadoSituacionLegal
            }) : Json(new
            {
                data = "ERROR"
            });
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerSituacionLegal(SituacionLegalPropiedad situacionLegalPropiedad)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.ObtenerSituacionLegalPropiedad(situacionLegalPropiedad.IdSituacionLegalPropiedad);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, data = respuesta.Resultado as SituacionLegalPropiedad });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = "No existe" });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDatosSituacionPropiedad(SituacionLegalPropiedad situacion)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosSituacionPropiedad(situacion);

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

        [HttpPost]
        public async Task<IActionResult> AgregarDatosEditadosSituacionPropiedad(SituacionLegalPropiedad situacion)
        {

            Response respuesta = await contenedorTrabajo.Propiedades.AgregarDatosEditadosSituacionPropiedad(situacion);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = "No existe" });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> EliminarTipoSituacionPropiedad(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Propiedades.EliminarTipoSituacionPropiedad(id.Value);

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

        /**********************************************************************************************************************/

        [HttpPost]
        public async Task<IActionResult> CambiarEstadoPropiedad(MostrarPropiedadTabla propiedad)
        {
            if (propiedad != null)
            {
                Response respuesta = await contenedorTrabajo.Propiedades.CambiarEstadoPropiedad(propiedad);

                return respuesta.EsCorrecto ? Json(new
                {
                    success = respuesta.EsCorrecto,
                    message = respuesta.Mensaje
                }) : Json(new
                {
                    success = respuesta.EsCorrecto,
                    message = respuesta.Mensaje
                });
            }

            return Json(new { success = false });

        }

        [HttpPost]
        public async Task<IActionResult> CambiarPublicadoPropiedad(MostrarPropiedadTabla propiedad)
        {
            if (propiedad != null)
            {
                Response respuesta = await contenedorTrabajo.Propiedades.CambiarPublicadoPropiedad(propiedad);

                return respuesta.EsCorrecto ? Json(new
                {
                    success = respuesta.EsCorrecto,
                    message = respuesta.Mensaje
                }) : Json(new
                {
                    success = respuesta.EsCorrecto,
                    message = respuesta.Mensaje
                });
            }

            return Json(new { success = false });

        }

        [HttpPost]
        public async Task<IActionResult> EliminarImagen(Imagen imagen)
        {

            Response imagenObtenida = await contenedorTrabajo.Propiedades.ObtenerImagen(imagen.IdImagen);

            if (imagenObtenida.EsCorrecto)
            {
                string rutaDirectorioPrincipal = environment.WebRootPath;
                Imagen img = imagenObtenida.Resultado as Imagen;
                string rutaImagen = Path.Combine(rutaDirectorioPrincipal, img.Ruta.TrimStart('\\'));

                if (System.IO.File.Exists(rutaImagen))
                {
                    System.IO.File.Delete(rutaImagen);
                }

                Response respuesta = await contenedorTrabajo.Propiedades.EliminarImagenGaleria(imagenObtenida.Resultado as Imagen);

                if (respuesta.EsCorrecto)
                {
                    Response respuesta2 = await contenedorTrabajo.Propiedades.ObtenerImagenesPropiedad(imagen.IdPropiedad);

                    if (respuesta2.EsCorrecto)
                    {
                        List<Imagen> listadoImagenes = (List<Imagen>)respuesta2.Resultado;
                        return PartialView("~/Areas/Admin/Views/Propiedad/_ListadoImagenes.cshtml", listadoImagenes);
                    }
                }

            }

            return PartialView("~/Areas/Admin/Views/Propiedad/_ListadoImagenes.cshtml", new List<Imagen>());

        }

        [HttpGet]
        public ActionResult ObtenerListaIntermediariosPropiedad()
        {
            List<IntermediarioTabla> listaIntermediarios = contenedorTrabajo.Intermediarios.ObtenerListaIntermediariosPropiedad(idPropiedad);
            return Json(new { data = listaIntermediarios });
        }

    }
}
