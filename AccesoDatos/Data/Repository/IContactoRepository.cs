using AccesoDatos.BlogCore.Models;
using Modelos.ViewModels.Clientes;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Repository
{
    public interface IContactoRepository : IRepository<TbContacto>
    {

        public Task<string> RegistrarContacto(RegistrarContactoViewModel model);


    }
}
