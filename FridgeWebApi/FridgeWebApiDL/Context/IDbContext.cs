using FridgeWebApiDL.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FridgeWebApiDL.Context
{
    public interface IDbContext
    {
        string GetConnectionString { get; }
        string GetDatabase { get; }
        IRepository<T> DbSet<T>() where T : class, IEntity, new();
        Task ExecuteQuery(CancellationToken token = default, params string[] queries);

        Task<ICollection<Entity>> ExecuteQueryAndRead<Entity>(string query, CancellationToken token = default)
            where Entity : class, IEntity, new();

        Task<ICollection<Entity>> ExecuteQueryAndRead<Entity, JoinEntity>(string query, CancellationToken token = default)
            where Entity : class, IEntity, new()
            where JoinEntity : class, IEntity, new();

        Task<Dto> TryExecuteProcedure<Dto>(
            string nameProcedure,
            DbContext.CreateProcedure createProcedure, DbContext.ConvertIntoEntity<Dto> getEntity,
            CancellationToken token = default)
            where Dto : class, new();
    }
}