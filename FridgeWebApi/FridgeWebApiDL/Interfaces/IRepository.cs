using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FridgeWebApiDL.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<ICollection<T>> GetAll(CancellationToken token = default);
        Task<T> Get(int id, CancellationToken token = default);
        Task Create(T item, CancellationToken token = default);
        Task Update(T item, CancellationToken token = default);
        Task Delete(int id, CancellationToken token = default);
    }
}
