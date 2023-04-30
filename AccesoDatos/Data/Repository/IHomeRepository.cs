using Modelos.Entidades;
using Modelos.ViewModels.FrontEnd.Home;
using Modelos.ViewModels.Propiedades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Repository
{
    public interface IHomeRepository
    {
        public Task<List<VerPropiedadViewModel>> ObtenerDatosPropiedadesRecientes(string intencion, string proc);
        public Task<List<VerPropiedadViewModel>> ObtenerPropiedadesBusquedaLugarIntencion(string buscar, string intencion, string seleccionado, string proc);
        public Task<InformacionPropiedadViewModel> ObtenerInformacionPropiedad(int id);
        public Task<InformacionConstruccionViewModel> ObtenerInformacionConstruccion(int id);
        public TotalPorProvincia TotalPorProvincia();
        public List<MostrarPropiedadTabla> ObtenerListaPropiedadesHome();
        public List<ListaUsosPropiedades> ObtenerListaUsosPropiedadesAlquiler();
        public List<ListaUsosPropiedades> ObtenerListaUsosPropiedadesVenta();
        public List<ListaUsosPropiedades> Test(string intencion);
        public List<string> ObtenerFiltroBusqueda(string busqueda, string intencion);
    }
}
