using AccesoDatos.BlogCore.Models;
using Modelos.ViewModels.Propiedades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Repository
{
    public interface IAlquilerRepository : IRepository<TbPropiedade>
    {
        public Task<VerPropiedadViewModel> ObtenerInformacionAlquilerPropiedad(int id);
        public Task<List<VerPropiedadViewModel>> ObtenerDatosPropiedadesAlquiler(int id);
    }
}
