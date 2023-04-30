using System.Collections.Generic;

namespace AccesoDatos.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(int? id);

        IEnumerable<T> GetAll();

        T GetFirstOrDefault();

        void Add(T Entity);

        void Remove(int? id);

        void Remove(T entity);

    }
}

