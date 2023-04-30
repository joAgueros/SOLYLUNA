using AccesoDatos.BlogCore.Models;
using Modelos.Entidades;
using Modelos.ViewModels.Agendas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoDatos.Data.Repository
{
    public interface IAgendaRepository : IRepository<Event>
    {
        public Task<Response> AddEvent(Eventos evt);
        public List<Eventos> GetCalendarEvents(string start, string end);
        public Task<Response> UpdateEvents(Eventos evt);
        public Task<Response> DeleteEvents(int eventId);
    }
}
