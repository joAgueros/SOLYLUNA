using AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.Entidades;
using Modelos.ViewModels.FrontEnd.Propiedades;
using Modelos.ViewModels.Propiedades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitioWeb.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class PropiedadesController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public PropiedadesController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        public async Task<IActionResult> Propiedades(int? id)
        {
            PropiedadesBusquedaViewModel datosIndex = new()
            {
                DatosPropiedad = await contenedorTrabajo.Propiedades.ObtenerDatosPropiedadesVenta(id.Value),
                TiposMoneda = contenedorTrabajo.ComboBox.ObtenerTiposMoneda(),
                ValoresMaximos = contenedorTrabajo.ComboBox.ObtenerRangosColones(),
                ValoresMinimos = contenedorTrabajo.ComboBox.ObtenerRangosColones()
            };

            return View(datosIndex);

        }

        public async Task<IActionResult> BusquedaPropiedades(string buscar, string intencion, string seleccionado)
        {
            if (string.IsNullOrEmpty(buscar) && seleccionado.Equals("01"))
            {
                List<VerPropiedadViewModel> listadoGeneral = await contenedorTrabajo.HomeCliente.ObtenerDatosPropiedadesRecientes(string.Empty, "Proc2");

                if (listadoGeneral.Count > 0)
                {
                    listadoGeneral[0].VanTodas = true;
                }

                return View(listadoGeneral);
            }

            List<VerPropiedadViewModel> listadoPropiedades = await contenedorTrabajo.HomeCliente.
                ObtenerPropiedadesBusquedaLugarIntencion(buscar, intencion, seleccionado, "Proc1");

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

        [HttpPost]
        public async Task<IActionResult> ObtenerPropiedadPorFiltrado(BuscarPropiedad propiedad)
        {

            if (propiedad.Provincia.Equals("Todas") && propiedad.Tipo.Equals("Todas"))
            {
                propiedad.Proc = "Proc1";
            }
            else if (propiedad.Provincia.Equals("Todas") && !propiedad.Tipo.Equals("Todas"))
            {
                propiedad.Proc = "Proc2";
            }
            else if (!propiedad.Provincia.Equals("Todas") && propiedad.Tipo.Equals("Todas"))
            {
                propiedad.Proc = "Proc3";
            }
            else if (!propiedad.Provincia.Equals("Todas") && !propiedad.Tipo.Equals("Todas"))
            {
                propiedad.Proc = "Proc4";
            }

            List<VerPropiedadViewModel> listado = await contenedorTrabajo.Propiedades.ObtenerPropiedadPorFiltrado(propiedad);

            if (listado == null)
            {
                return BadRequest();
            }

            return PartialView("~/Areas/Cliente/Views/Propiedades/_ListadoPropiedades.cshtml", listado);
        }

        [HttpGet]
        public IActionResult ObtenerListaPropiedadesHome()
        {
            List<MostrarPropiedadTabla> listadoPropiedades = contenedorTrabajo.HomeCliente.ObtenerListaPropiedadesHome();

            if (listadoPropiedades != null)
            {
                return Json(new { data = listadoPropiedades });
            }
            else
            {
                return Json(new { data = "ERROR" });
            }
        }

        [HttpPost]
        public IActionResult ObtenerValores(string tipoMoneda)
        {
            List<SelectListItem> listado;

            if (tipoMoneda.Equals("₡ Colones"))
            {
                listado = contenedorTrabajo.ComboBox.ObtenerRangosColones();
            }
            else
            {
                listado = contenedorTrabajo.ComboBox.ObtenerRangosDolares();
            }

            return Json(new { data = listado });
        }

    }
}
