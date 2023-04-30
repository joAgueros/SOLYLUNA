using AccesoDatos.BlogCore.Models;
using AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Modelos.ViewModels.Clientes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoDatos.Data.EntidadesImplementadas
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly SolyLunaDbContext Context;
        internal DbSet<T> dbSet;

        public Repository(SolyLunaDbContext context)
        {
            Context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T Entity)
        {
            dbSet.Add(Entity);
        }

        public T Get(int? id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public Task<string> RegistrarContacto(RegistrarContactoViewModel model)
        {
            throw new System.NotImplementedException();
        }
        public T GetFirstOrDefault()
        {
            IQueryable<T> query = dbSet;
            return query.FirstOrDefault();
        }


        public void Remove(int? id)
        {
            T entityToRemove = dbSet.Find(id);
            Remove(entityToRemove);
        }
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}