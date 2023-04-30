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
    public class ConstruccionController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        private readonly IWebHostEnvironment environment;
        private static int idConstruccion = 0;

        public ConstruccionController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment environment)
        {
            this.contenedorTrabajo = contenedorTrabajo;
            this.environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {

            if (id == null)
            {
                return new NotFoundViewResult("ConstruccionNotFound");
            }

            idConstruccion = id.Value;

            Construccion construccionDatos = await contenedorTrabajo.Construcciones.ObtenerConstruccion(id.Value);

            if (construccionDatos == null)
            {
                return new NotFoundViewResult("ConstruccionNotFound");
            }

            ConstruccionViewModel construccionModel = new()
            {
                Id = id.Value,
                EstadoFisicoConstruccion = contenedorTrabajo.ComboBox.ObtenerTiposEstadoFisicoConstruccion(),
                Periodo = contenedorTrabajo.ComboBox.ObtenerListaPeriodos(),
                TipoMedida = await contenedorTrabajo.ComboBox.ObtenerTiposMedidas(),
                Visualizacion = await contenedorTrabajo.ComboBox.ObtenerListaVisualizaciones(),
                Cableado = await contenedorTrabajo.ComboBox.ObtenerListaTipoCableado(),
                Estado = construccionDatos.Estado,
                Antiguedad = construccionDatos.Antiguedad,
                Descripcion = construccionDatos.Descripcion,
                VisualizacionId = construccionDatos.IdVisualizacion,
                Siglas = construccionDatos.Siglas,
                TipoMedidadId = construccionDatos.IdMedida,
                Medida = construccionDatos.Medida,
                EstadoFisico = construccionDatos.EstadoFisico,
                TipoVisualizacion = construccionDatos.Vistada,
                FechaRegistra = construccionDatos.FechaRegistra,
                Status = construccionDatos.Estado.Equals("Activo")
            };

            if (!string.IsNullOrEmpty(construccionDatos.EstadoFisico))
            {
                Microsoft.AspNetCore.Mvc.Rendering.SelectListItem estadoFisico = contenedorTrabajo.ComboBox.ObtenerTiposEstadoFisicoConstruccion()
                .FirstOrDefault(x => x.Text.StartsWith("A"));
                construccionModel.EstadoFisicoId = Convert.ToInt32(estadoFisico.Value);
            }

            string[] array = construccionDatos.Antiguedad.Split("-");
            if (array[0] != string.Empty)
            {
                string cantidadPeriodo = array[0];
                string per = array[1];
                Microsoft.AspNetCore.Mvc.Rendering.SelectListItem periodo = contenedorTrabajo.ComboBox.ObtenerListaPeriodos().FirstOrDefault(x => x.Text.Equals(per));

                construccionModel.PeriodoId = Convert.ToInt32(periodo.Value);
                construccionModel.TotalPeriodo = Convert.ToDecimal(cantidadPeriodo);
            }

            return View(construccionModel);

        }

        [HttpGet]
        public async Task<IActionResult> Galeria(int? id)
        {

            if (id == null)
            {
                return new NotFoundViewResult("ConstruccionNotFound");
            }

            /*validar que la propiedad exista*/
            AccesoDatos.BlogCore.Models.TbConstruccion existe = contenedorTrabajo.Construcciones.Get(id.Value);

            if (existe == null)
            {
                return new NotFoundViewResult("ConstruccionNotFound");
            }

            Response respuesta = await contenedorTrabajo.Construcciones.ObtenerImagenesConstruccion(id.Value);

            if (respuesta.EsCorrecto)
            {
                GaleriaViewModel galeria = new()
                {
                    IdConstruccion = id.Value,
                    Imagenes = (List<Imagen>)respuesta.Resultado
                };

                return View(galeria);
            }

            return new NotFoundViewResult("ConstruccionNotFound");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarImagen(Imagen imagen)
        {

            Response imagenObtenida = await contenedorTrabajo.Construcciones.ObtenerImagen(imagen.IdImagen);

            if (imagenObtenida.EsCorrecto)
            {
                string rutaDirectorioPrincipal = environment.WebRootPath;
                Imagen img = imagenObtenida.Resultado as Imagen;
                string rutaImagen = Path.Combine(rutaDirectorioPrincipal, img.Ruta.TrimStart('\\'));

                if (System.IO.File.Exists(rutaImagen))
                {
                    System.IO.File.Delete(rutaImagen);
                }

                Response respuesta = await contenedorTrabajo.Construcciones.EliminarImagenGaleria(imagenObtenida.Resultado as Imagen);

                if (respuesta.EsCorrecto)
                {
                    Response respuesta2 = await contenedorTrabajo.Construcciones.ObtenerImagenesConstruccion(imagen.IdConstruccion);

                    if (respuesta2.EsCorrecto)
                    {
                        List<Imagen> listadoImagenes = (List<Imagen>)respuesta2.Resultado;
                        return PartialView("~/Areas/Admin/Views/Construccion/_ListadoImagenes.cshtml", listadoImagenes);
                    }
                    else
                    {
                        return Json(new { success = respuesta2.EsCorrecto });
                    }
                }

            }

            return PartialView("~/Areas/Admin/Views/Construccion/_ListadoImagenes.cshtml", new List<Imagen>());

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

                string nombreCarpeta = $@"{rutaPrincipal}\imagenes\construcciones";
                string pathString = Path.Combine(nombreCarpeta, galeria.IdConstruccion.ToString());

                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
                //nueva imagen
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
                string rutaImagenPesada = $@"{rutaPrincipal}\imagenes\construcciones\{galeria.IdConstruccion}\{nombreArchivo}{extension}";
                //Ruta corta de la imagen guardada
                string rutaImagenAux = $@"\imagenes\construcciones\{galeria.IdConstruccion}\{nombreArchivo}{extension}";

                //Nombre único para nombre de imagen redimencionada
                string nombreArchivo2 = Guid.NewGuid().ToString();
                //Ruta Corta que se almacenará en BD
                string rutaImagenBD = $@"\imagenes\construcciones\{galeria.IdConstruccion}\{nombreArchivo2}{extension}";
                //Ruta con nuevo nombre de la imagen
                string rutaImagenResize = $@"{rutaPrincipal}\imagenes\construcciones\{galeria.IdConstruccion}\{nombreArchivo2}{extension}";

                //Metodo para redimencionar imagen
                using (MagickImage oMagickImage = new(rutaImagenPesada))
                {
                    if (oMagickImage.Width > 650)
                    {
                        oMagickImage.Resize(650, 0);
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


                Response respuesta = await contenedorTrabajo.Construcciones.AgregarImagenConstruccion(rutaImagenBD, galeria.IdConstruccion);


                if (respuesta.EsCorrecto)
                {
                    Response respuesta2 = await contenedorTrabajo.Construcciones.ObtenerImagenesConstruccion(galeria.IdConstruccion);

                    if (respuesta2.EsCorrecto)
                    {
                        List<Imagen> listadoImagenes = (List<Imagen>)respuesta2.Resultado;
                        return PartialView("~/Areas/Admin/Views/Construccion/_ListadoImagenes.cshtml", listadoImagenes);
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

        [HttpPost]
        public async Task<IActionResult> CambiarEstadoConstruccion(Construccion construccion)
        {
            if (construccion != null)
            {
                Response respuesta = await contenedorTrabajo.Construcciones.CambiarEstadoConstruccion(construccion);

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

        [HttpPost]
        public async Task<IActionResult> AgregarDatosBasicosConstruccion(EditarConstruccion construccion)
        {

            try
            {
                Response respuesta = await contenedorTrabajo.Construcciones.AgregarDatosBasicosConstruccion(construccion);

                if (respuesta.EsCorrecto)
                {
                    return Json(new { success = true, message = respuesta.Mensaje });
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

        /*EQUIPAMIENTO*/

        [HttpPost]
        public async Task<IActionResult> AgregarEquipamiento(ConstruccionEquipamiento equipamiento)
        {

            try
            {
                bool respuesta = await contenedorTrabajo.Construcciones.AgregarEquipamiento(
                    equipamiento.IdEquipamiento, equipamiento.IdConstruccion);

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
        public ActionResult ObtenerListaEquipamientosAdquiridos()
        {
            List<ConstruccionEquipamiento> listadoPropiedades = contenedorTrabajo.Construcciones.ObtenerEquipamientosAdquiridos(idConstruccion);

            if (listadoPropiedades != null)
            {
                return Json(new { data = listadoPropiedades });
            }
            else
            {
                return Json(new { data = "ERROR" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListaEquipamientos()
        {
            List<AccesoDatos.BlogCore.Models.TbEquipamiento> listadoEquipamientos = await contenedorTrabajo.Construcciones.ObtenerTodosLosEquipamientos();

            if (listadoEquipamientos != null)
            {
                return Json(new { data = listadoEquipamientos.OrderBy(X => X.Descripcion) });
            }
            else
            {
                return Json(new { data = "ERROR" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AumentarEquipamiento(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Construcciones.AumentarEquipamiento(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido aumentado correctamente" });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar aumentar la cantidad del registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> DisminuirEquipamiento(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Construcciones.DisminuirEquipamiento(id.Value);

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
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar disminuir el registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> EliminarEquipamiento(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Construcciones.EliminarEquipamiento(id.Value);

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

        /****************************************************************************************************************/


        /*DIVISIONES*/

        [HttpPost]
        public async Task<IActionResult> AgregarDivisiones(Division division)
        {
            if (division != null)
            {
                Response respuesta = await contenedorTrabajo.Construcciones.AgregarDivision(division);

                if (respuesta.EsCorrecto)
                {
                    return Json(new { success = true, message = "OK" });
                }
                else
                {
                    return Json((success: false, message: respuesta));
                }
            }

            return Json(new { success = false });

        }

        [HttpPost]
        public async Task<IActionResult> ObtenerDatosDivisionEspecifica(ConstruccionDivision construccionDivision)
        {
            Response respuesta = await contenedorTrabajo.Construcciones.ObtenerDivisionAdquirida(
                                   construccionDivision.IdConstruccion, construccionDivision.IdConstruccionDivision);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = respuesta.EsCorrecto, data = respuesta.Resultado as ConstruccionDivision });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}" });
            }

        }

        [HttpGet]
        public ActionResult ObtenerListaDivisionesAdquiridas()
        {
            List<ConstruccionDivision> listadoDivisionesAdquiridas = contenedorTrabajo.Construcciones.ObtenerDivisionesAdquiridas(idConstruccion);

            return Json(new { data = listadoDivisionesAdquiridas });

        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListaDivisiones()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listadoDivisiones = await contenedorTrabajo.ComboBox.ObtenerDivisionesConstruccion();

            return Json(new { data = listadoDivisiones });

        }

        [HttpPost]
        public async Task<IActionResult> EliminarDivisionAgregada(Division division)
        {

            Response respuesta = await contenedorTrabajo.Construcciones.EliminarDivisionAgregada(division);

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

        [HttpPost]
        public async Task<IActionResult> EditarDivision(ConstruccionDivision division)
        {

            Response respuesta = await contenedorTrabajo.Construcciones.EditarDivision(division);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje });
            }
            else
            {
                return respuesta.Mensaje.Equals("No existe") ? Json(new
                {
                    success = false,
                    message = "No existe"
                }) : Json(new
                {
                    success = false,
                    message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}"
                });
            }

        }

        /***********************************************************************************************************/

        /*MATERIALES (conjunto con las divisiones, cada division tiene sus materiales respectivos)*/


        [HttpGet]
        public async Task<ActionResult> ObtenerListaMateriales()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listadoMateriales = await contenedorTrabajo.ComboBox.ObtenerMaterialesObra();

            return Json(new { data = listadoMateriales });

        }

        [HttpPost]
        public async Task<IActionResult> AgregarMaterialDivision(Material division)
        {
            if (division != null)
            {
                Response respuesta = await contenedorTrabajo.Construcciones.AgregarMaterialDivision(division);

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

        [HttpPost]
        public async Task<IActionResult> EliminarMaterialDivision(Material division)
        {

            Response respuesta = await contenedorTrabajo.Construcciones.EliminarMaterialDivision(division);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido eliminado correctamente" });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe division"))
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                if (respuesta.Mensaje.Equals("No existe material"))
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }


                return Json(new { success = false, message = $"Ha ocurrido un error al intentar eliminar el registro : {respuesta.Mensaje}" });
            }

        }

        /*********************************************************************************************************************/

        /*CARACTERISTICAS*/

        public async Task<IActionResult> AgregarCaracteristicaConstruccion(ConstruccionCaracteristica caracteristica)
        {

            try
            {
                bool respuesta = await contenedorTrabajo.Construcciones.AgregarCaracteristicaConstruccion(
                    caracteristica.IdCaracteristica, caracteristica.IdConstruccion);

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
        public ActionResult ObtenerListaCaracteristicasConstruccionAdquiridas()
        {
            List<ConstruccionCaracteristica> listadoPropiedades = contenedorTrabajo.Construcciones.ObtenerCaracteristicasConstruccionAdquiridas(idConstruccion);

            if (listadoPropiedades != null)
            {
                return Json(new { data = listadoPropiedades });
            }
            else
            {
                return Json(new { data = "ERROR" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListaCaracteristicasConstruccion()
        {
            List<AccesoDatos.BlogCore.Models.TbCaracteristica> listadoCaracteristicas = await contenedorTrabajo.Construcciones.ObtenerTodasLasCaracteristicas();

            if (listadoCaracteristicas != null)
            {
                return Json(new { data = listadoCaracteristicas.OrderBy(X => X.Descripcion) });
            }
            else
            {
                return Json(new { data = "ERROR" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AumentarCaracteristicaConstruccion(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Construcciones.AumentarCaracteristicaConstruccion(id.Value);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = "El registro ha sido aumentado correctamente" });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar aumentar la cantidad del registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> DisminuirCaracteristicaConstruccion(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Construcciones.DisminuirCaracteristicaConstruccion(id.Value);

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
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar disminuir el registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> EliminarCaracteristicaConstruccion(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No existe" });
            }

            Response respuesta = await contenedorTrabajo.Construcciones.EliminarCaracteristicaConstruccion(id.Value);

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


        /***********************************************************************************************************************/

        /*CABLEADO*/

        [HttpPost]
        public async Task<IActionResult> EliminarTipoCableadoAdquirido(Cableado cableado)
        {

            Response respuesta = await contenedorTrabajo.Construcciones.EliminarCableadoConstruccion(cableado);

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

        [HttpGet]
        public ActionResult ObtenerListadoTiposCableadoObtenidos()
        {
            List<ConstruccionCableado> listadoCableadosAdquiridos = contenedorTrabajo.Construcciones.ObtenerListadoTiposCableadoObtenidos(idConstruccion);

            return Json(new { data = listadoCableadosAdquiridos });

        }

        [HttpPost]
        public async Task<IActionResult> ObtenerDatosCableadoAgregado(Cableado cableado)
        {
            Response respuesta = await contenedorTrabajo.Construcciones.ObtenerTipoCableadoAdquirido(cableado);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = respuesta.EsCorrecto, data = respuesta.Resultado as TiposCableado });
            }
            else
            {
                if (respuesta.Mensaje.Equals("No existe"))
                {
                    return Json(new { success = false, message = respuesta.Mensaje });
                }

                return Json(new { success = false, message = $"Ha ocurrido un error al intentar acceder al registro : {respuesta.Mensaje}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AgregarTiposDeCableadoConstruccion(Cableado cableado)
        {
            try
            {
                if (cableado != null)
                {
                    Response respuesta = await contenedorTrabajo.Construcciones.AgregarTiposDeCableadoConstruccion(cableado);

                    if (respuesta.EsCorrecto)
                    {
                        return Json(new { success = respuesta.EsCorrecto, message = respuesta.EsCorrecto });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListaTiposCableado()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> listadoTipoCableado = await contenedorTrabajo.ComboBox.ObtenerListaTipoCableado();

            return Json(new { data = listadoTipoCableado });

        }

        [HttpPost]
        public async Task<IActionResult> EditarTipoCableado(TiposCableado cableado)
        {

            Response respuesta = await contenedorTrabajo.Construcciones.EditarCableadoConstruccion(cableado);

            if (respuesta.EsCorrecto)
            {
                return Json(new { success = true, message = respuesta.Mensaje });
            }
            else
            {
                return respuesta.Mensaje.Equals("No existe") ? Json(new
                {
                    success = false,
                    message = "No existe"
                }) : Json(new
                {
                    success = false,
                    message = $"Ha ocurrido un error al intentar editar el registro : {respuesta.Mensaje}"
                });
            }
        }


        /*****************************************************************************************************************************/


    }
}
