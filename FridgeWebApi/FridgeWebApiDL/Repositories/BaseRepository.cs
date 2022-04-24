using FridgeWebApiDL.Helper;
using FridgeWebApiDL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FridgeWebApiDL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly string connectionString;
        protected BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public abstract string QueryGetAll(string nameDatabase);
        public abstract string QueryGet(int id, string nameDatabase);
        public abstract string QueryCreate(T item, string nameDatabase);
        public abstract string QueryUpdate(T item, string nameDatabase);
        public abstract string QueryDelete(int id, string nameDatabase);

        public virtual async Task<ICollection<T>> GetAll(CancellationToken token = default)
        {
            await using var connect = new SqlConnection(connectionString);
            await connect.OpenAsync(token);
            return await connect.ExecuteQueryAndRead<T>(QueryGetAll(connect.Database), token);
        }

        public virtual async Task<T> Get(int id, CancellationToken token = default)
        {
            await using var connect = new SqlConnection(connectionString);
            await connect.OpenAsync(token);
            var element = await connect.ExecuteQueryAndRead<T>(QueryGet(id ,connect.Database), token);
            return element.FirstOrDefault();
        }

        public virtual async Task Create(T item, CancellationToken token = default)
        {
            await using var connect = new SqlConnection(connectionString);
            await connect.OpenAsync(token);
            await connect.ExecuteQuery(token, this.QueryCreate(item, connect.Database));
        }

        public virtual async Task Update(T item, CancellationToken token = default)
        {
            await using var connect = new SqlConnection(connectionString);
            await connect.OpenAsync(token);
            await connect.ExecuteQuery(token, this.QueryUpdate(item, connect.Database));
        }
            
        public virtual async Task Delete(int id, CancellationToken token = default)
        {
            await using var connect = new SqlConnection(connectionString);
            await connect.OpenAsync(token);
            await connect.ExecuteQuery(token, this.QueryDelete(id, connect.Database));
        }
    }
}
