using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceStation.BLL
{
    public interface IEntityService<T> where T : class
    {
        Task Create(T item);
        Task Delete(int id);
        Task Update(T item);
        Task<T> GetItem(int id);
        Task<IEnumerable<T>> GetItems();
    }
}
