using AccesoDatos.BlogCore.Models;
using Modelos.Entidades;
using Modelos.ViewModels.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Repository
{
    public interface ICompradoresRepository : IRepository<TbClienteComprador>
    {
        public Task<List<TbPersonaJuridica>> ObtenerTodosTiposPersonaJuridica();
        public Task<List<TbTipoIntermediario>> ObtenerTodosLosTiposIntermediarios();
        public List<ReferenciasViewModel> ObtenerReferenciasComprador(int idClienteComprador);
        public CompradorViewModel ObtenerComprador(int idClienteComprador);
        public Task<Response> RegistrarComprador(RegistrarCompradorViewModel comprador, string usuario);
        public List<CompradorTabla> ObtenerListaCompradores();
        public Task<EditarCompradorViewModel> ObtenerCompradorParaEditar(int idClienteComprador);
        public Task<Response> EditarRegistroComprador(EditarCompradorViewModel model, string usuario);
        public Task<Response> AgregarDatosPropiedadElegida(CaracteristicasPropiedadElegida caracteristicas, string usuario);
        public List<CaracteristicasPropiedadElegida> ObtenerCaracteristicasPropiedadObtenida(int idComprador);
        public Response ObtenerCaracteristicaPropiedadAdquirida(int id);
        public Task<Response> EliminarCaracteristicaPropiedadAdquirida(int id, string usuario);
        public Task<Response> EditarCaracteristicasPropiedadElegida(CaracteristicasPropiedadElegida model, string usuario);
        public Task<Response> AgregarDatosGestion(GestionCompra gestion, string usuario);
        public List<GestionCompra> ObtenerGestiones(int idComprador);
        public Task<Response> EliminarGestion(int id, string usuario);
        public Response ObtenerGestion(int id);
        public Task<Response> EditarGestion(GestionCompra model, string usuario);
        public Task<Response> AgregarDatosResultadoSugef(ResultadoSugef resultado, string usuario);
        public List<ResultadoSugef> ObtenerResultadosSugef(int idComprador);
        public Task<Response> EliminarResultadosSugef(int id, string usuario);
        public Response ObtenerResultadoSugef(int id);
        public Task<Response> EditarResultadoSugef(ResultadoSugef model, string usuario);
        public Task<Response> AgregarDatosResultadoSolicitante(ResultadoSolicitante resultado, string usuario);
        public List<ResultadoSolicitante> ObtenerResultadosSolicitante(int idComprador);
        public Task<Response> EliminarResultadosSolicitante(int id, string usuario);
        public Response ObtenerResultadoSolicitante(int id);
        public Task<Response> EditarResultadoSolicitante(ResultadoSolicitante model, string usuario);
        public Task<Response> AgregarDatosDocumentoComprador(DocumentoComprador documento, string usuario);
        public Task<Response> AgregarDatosEditadosDocumentosComprador(DocumentoComprador documento, string usuario);
        public List<DocumentoComprador> ObtenerDocumentosComprador(int idComprador);
        public Task<Response> ObtenerDocumentoComprador(int idDocumento);
        public Task<Response> EliminarDocumentoComprador(int idDocumento, string usuario);
        public Task<Response> CambiarEstadoReferencias(ReferenciasViewModel referencias, string usuario);

    }
}
