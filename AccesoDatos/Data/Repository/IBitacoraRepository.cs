using AccesoDatos.BlogCore.Models;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Repository
{
    public interface IBitacoraRepository : IRepository<TbBitacora>
    {
        public Task<bool> InsertarPropiedad(string usuario, string codigoIdentificadorPropiedad);
        public Task<bool> InsertarConstruccion(string usuario, string codigoIdentificadorConstruccion);
        public Task<bool> InsertarVendedor(string usuario, string codigoIdentificadorVendedor);
        public Task<bool> InsertarComprador(string usuario, string codigoIdentificadorVendedor);
        public Task<bool> EditarPropiedad(string usuario, string codigoIdentificadorPropiedad);
        public Task<bool> EditarConstruccion(string usuario, string codigoIdentificadorConstruccion);
        public Task<bool> EditarVendedor(string usuario, string codigoIdentificadorVendedor);
        public Task<bool> EditarComprador(string usuario, string codigoIdentificadorVendedor);
        public Task<bool> InformesModificacionBitacora(string usuario, string mensaje, string tablaAfectada, string operacion, int idTablaAfectada);
    }
}
