using AccesoDatos.BlogCore.Models;
using Modelos.Entidades;
using Modelos.ViewModels.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Repository
{
    public interface IVendedoresRepository : IRepository<TbClienteVendedor>
    {
        public Task<List<TbPersonaJuridica>> ObtenerTodosTiposPersonaJuridica();
        public Task<List<TbTipoIntermediario>> ObtenerTodosLosTiposIntermediarios();
        public List<ReferenciasViewModel> ObtenerReferenciasVendedor(int idClienteVendedor);
        public Task<Response> RegistrarVendedor(RegistrarVendedorViewModel model, string usuario);
        public Task<Response> EditarRegistroVendedor(EditarVendedorViewModel model, string usuario);
        public List<VendedorTabla> ObtenerListaVendedores();
        public List<MostrarPropiedadTabla> ObtenerListaPropiedadesPorClienteVendedor(int idClienteVendedor);
        public VendedorViewModel ObtenerVendedor(int idClienteVendedor);
        public Task<EditarVendedorViewModel> ObtenerVendedorParaEditar(int idClienteVendedor);
        public Task<Response> CambiarEstadoReferencias(ReferenciasViewModel referencias);
    }
}
