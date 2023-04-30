using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.ViewModels.Clientes;
using Modelos.ViewModels.Propiedades;
using SitioWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class VendedorController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public VendedorController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
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
                            return Json(new { success = mensaje.EsCorrecto, data = mensaje.Mensaje });
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
                        return Json(new { data = "Error" });
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
        public ActionResult ObtenerListaVendedores()
        {
            List<Modelos.Entidades.VendedorTabla> listadoVendedores = contenedorTrabajo.Vendedores.ObtenerListaVendedores();
            return Json(new { data = listadoVendedores });
        }

        [HttpGet]
        public async Task<IActionResult> VerRegistroVendedor(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VendedorNotFound");
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
                NivelCallePropiedad = contenedorTrabajo.ComboBox.ObtenerTiposNivelCalle(),
                TopografiaPropiedad = contenedorTrabajo.ComboBox.ObtenerTiposTopografia(),
                Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias(),
                Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(string.Empty),
                Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(string.Empty)
            };

            VendedorViewModel modelVendedor = contenedorTrabajo.Vendedores.ObtenerVendedor(id.Value);

            if (modelVendedor == null)
            {
                return new NotFoundViewResult("VendedorNotFound");
            }

            modelVendedor.TipoPersonas = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion();
            modelVendedor.Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            modelVendedor.Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(string.Empty);
            modelVendedor.Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(string.Empty);

            List<Modelos.Entidades.MostrarPropiedadTabla> listadoPropiedadesVendedor = contenedorTrabajo.Vendedores.ObtenerListaPropiedadesPorClienteVendedor(id.Value);
            List<ReferenciasViewModel> listaReferencias = contenedorTrabajo.Vendedores.ObtenerReferenciasVendedor(id.Value);

            return View(new VendedorPropiedadViewModel()
            {
                Propiedad = modelRegistrarPropiedad,
                Vendedor = modelVendedor,
                Propiedades = listadoPropiedadesVendedor,
                Referencias = listaReferencias ?? new List<ReferenciasViewModel>()
            });
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VendedorNotFound");
            }

            EditarVendedorViewModel clienteObtenido = await contenedorTrabajo.Vendedores.ObtenerVendedorParaEditar(id.Value);

            if (clienteObtenido == null)
            {
                return new NotFoundViewResult("VendedorNotFound");
            }

            clienteObtenido.TipoPersonas = await contenedorTrabajo.ComboBox.ObtenerTiposIdentificacion();
            clienteObtenido.Provincias = await contenedorTrabajo.ComboBox.ObtenerTodasLasProvincias();
            clienteObtenido.Cantones = await contenedorTrabajo.ComboBox.ObtenerTodosLosCantones(clienteObtenido.NombreProvicia.Trim());
            clienteObtenido.Distritos = await contenedorTrabajo.ComboBox.ObtenerTodosLosDistritos(clienteObtenido.NombreCanton.Trim());

            return View(clienteObtenido);
        }

        [HttpPost]
        public async Task<IActionResult> EditarRegistroVendedor(EditarVendedorViewModel vendedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modelos.Entidades.Response mensaje = await contenedorTrabajo.Vendedores.EditarRegistroVendedor(vendedor, User.Identity.Name);

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

                return View(vendedor);
            }
            catch (Exception)
            {
                return Json(new { data = "Error" });
            }

        }


        [HttpPost]
        public async Task<IActionResult> CambiarEstadoReferencia(ReferenciasViewModel referencias)
        {
            if (referencias != null)
            {
                Modelos.Entidades.Response respuesta = await contenedorTrabajo.Vendedores.CambiarEstadoReferencias(referencias);

                return respuesta.EsCorrecto
                    ? Json(new { success = respuesta.EsCorrecto, message = respuesta.Mensaje })
                    : Json(new { success = respuesta.EsCorrecto, message = respuesta.Mensaje });
            }

            return Json(new { success = false });

        }

    }
}
