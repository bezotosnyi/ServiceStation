using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiceStation.DAL.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ServiceStationContext serviceStationContext;

        public GenericRepository(ServiceStationContext context)
        {
            serviceStationContext = context;
        }

        public async Task Add(T item)
        {
            await serviceStationContext.Set<T>().AddAsync(item);
            await serviceStationContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = serviceStationContext.Set<T>().Find(id);
            if (item != null)
            {
                serviceStationContext.Set<T>().Remove(item);
            }
            await serviceStationContext.SaveChangesAsync();
        }

        public async Task Update(T item)
        {
            var entry = await serviceStationContext.Set<T>().FirstAsync(e => e.Id == item.Id);
            serviceStationContext.Entry(entry).CurrentValues.SetValues(item);
            await serviceStationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await serviceStationContext.Set<T>().ToListAsync();
        }


        public async Task<T> GetById(int id)
        {
            return await serviceStationContext.Set<T>().FindAsync(id);
        }
    }
}
