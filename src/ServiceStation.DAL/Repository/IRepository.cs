using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceStation.DAL.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task Add(T item);

        Task Update(T item);

        Task Delete(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);
    }
}
