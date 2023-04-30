using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> ObtenerTodasLasProvincias();
        Task<IEnumerable<SelectListItem>> ObtenerTodosLosCantones(string nombreProvincia);
        Task<IEnumerable<SelectListItem>> ObtenerTodosLosDistritos(string nombreCanton);
        Task<List<SelectListItem>> ObtenerTiposIdentificacion();
        Task<List<SelectListItem>> ObtenerTiposPropiedad();
        Task<List<SelectListItem>> ObtenerListaVisualizaciones();
        Task<List<SelectListItem>> ObtenerTiposUsoPropiedad();
        List<SelectListItem> ObtenerTiposIngresoPropiedad();
        Task<List<SelectListItem>> ObtenerListaTipoCableado();
        Task<List<SelectListItem>> ObtenerTiposMedidas();
        Task<List<SelectListItem>> ObtenerTiposUsoSueloPropiedad();
        Task<List<SelectListItem>> ObtenerDivisionesConstruccion();
        Task<List<SelectListItem>> ObtenerListaCuotas();
        Task<List<SelectListItem>> ObtenerListaServiciosMunicipales();
        Task<List<SelectListItem>> ObtenerTipoDocumentosPropiedad();
        Task<List<SelectListItem>> ObtenerMaterialesObra();
        Task<List<SelectListItem>> ObtenerListaSituacionPropiedad();
        Task<List<SelectListItem>> ObtenerListaServiciosPublicos();
        Task<List<SelectListItem>> ObtenerListaTiposIntermediarios();
        Task<List<SelectListItem>> ObtenerListaAccesibilidades();
        List<SelectListItem> ObtenerTiposEstadoIngresoPropiedad();
        List<SelectListItem> ObtenerTiposPozoPropiedad();
        List<SelectListItem> ObtenerTiposEstadoPozoPropiedad();
        List<SelectListItem> ObtenerTiposTopografia();
        List<SelectListItem> ObtenerTiposNivelCalle();
        List<SelectListItem> ObtenerTiposEstadoFisicoConstruccion();
        List<SelectListItem> ObtenerListaPeriodos();
        List<SelectListItem> ObtenerTiposMoneda();
        List<SelectListItem> ObtenerRangosColones();
        List<SelectListItem> ObtenerRangosDolares();
    }
}
