using AccesoDatos.BlogCore.Models;
using Modelos.Entidades;
using Modelos.ViewModels.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Repository
{
    public interface IIntermedarioRepository : IRepository<TbIntermediario>
    {
        public Task<List<TbPersonaJuridica>> ObtenerTodosTiposPersonaJuridica();
        public Task<List<TbTipoIntermediario>> ObtenerTodosLosTiposIntermediarios();
        public Task<Response> RegistrarIntermediario(RegistrarIntermediarioViewModel model);
        public Task<Response> EditarRegistroIntermediario(EditarIntermediarioViewModel model);
        public List<IntermediarioTabla> ObtenerListaIntermediarios();
        public IntermediarioViewModel ObtenerIntermediario(int idIntermediario);
        public Task<EditarIntermediarioViewModel> ObtenerIntermediarioParaEditar(int idIntermediario);
        public List<MostrarPropiedadTabla> ObtenerListaPropiedadesPorIntermediario(int idIntermediario);
        public Task<Response> AgregarIntermediarioPropiedad(RegistrarIntermediarioViewModel intermediario);
        public List<IntermediarioTabla> ObtenerListaIntermediariosPropiedad(int idPropiedad);
        public Task<Response> EliminarIntermediarioPropiedad(int idIntermediario);

    }
}
