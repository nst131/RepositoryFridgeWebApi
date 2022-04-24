using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using FridgeWebApiDL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FridgeWebApiDL.Context
{
    public class DbContext : IDbContext
    {
        private readonly IRepository<Fridge> FridgeRepository;
        private readonly IRepository<Products> ProductRepository;
        private readonly IRepository<FridgeProducts> FridgeProductsRepository;
        private readonly IRepository<User> UserRepository;
        private readonly IRepository<FridgeModel> FridgeModelRepository;
        public string GetConnectionString { get; }
        public string GetDatabase { get; }

        public delegate Task CreateProcedure();
        public delegate Dto ConvertIntoEntity<out Dto>(SqlDataReader reader) where Dto : class, new();
        public DbContext(IServiceProvider provider, string connectionString)
        {
            this.GetConnectionString = connectionString;
            this.GetDatabase = new SqlConnectionStringBuilder(connectionString).InitialCatalog;
            this.FridgeRepository = ActivatorUtilities.GetServiceOrCreateInstance<IRepository<Fridge>>(provider);
            this.ProductRepository = ActivatorUtilities.GetServiceOrCreateInstance<IRepository<Products>>(provider);
            this.FridgeProductsRepository = ActivatorUtilities.GetServiceOrCreateInstance<IRepository<FridgeProducts>>(provider);
            this.UserRepository = ActivatorUtilities.GetServiceOrCreateInstance<IRepository<User>>(provider);
            this.FridgeModelRepository = ActivatorUtilities.GetServiceOrCreateInstance<IRepository<FridgeModel>>(provider);
        }

        public IRepository<T> DbSet<T>() where T : class, IEntity, new()
        {
            var o = typeof(DbContext).GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(p => p.FieldType == typeof(IRepository<T>));

            if (o is null)
                throw new NullReferenceException();

            var element = o.FirstOrDefault()?.GetValue(this) as IRepository<T>;

            return element;
        }

        public async Task ExecuteQuery(CancellationToken token = default ,params string[] queries)
        {
            await using var connect = new SqlConnection(this.GetConnectionString);
            await connect.OpenAsync(token);
            await connect.ExecuteQuery(token, queries);
        }

        public async Task<ICollection<Entity>> ExecuteQueryAndRead<Entity>(string query, CancellationToken token = default)
            where Entity : class, IEntity, new()
        {
            await using var connect = new SqlConnection(this.GetConnectionString);
            await connect.OpenAsync(token);
            return await connect.ExecuteQueryAndRead<Entity>(query, token);
        }

        public async Task<ICollection<Entity>> ExecuteQueryAndRead<Entity, JoinEntity>(string query, CancellationToken token = default)
            where Entity : class, IEntity, new()
            where JoinEntity : class, IEntity, new()
        {
            await using var connect = new SqlConnection(this.GetConnectionString);
            await connect.OpenAsync(token);
            return await connect.ExecuteQueryAndRead<Entity, JoinEntity>(query, token);
        }

        public async Task<Dto> TryExecuteProcedure<Dto>(
            string nameProcedure,
            CreateProcedure createProcedure,
            ConvertIntoEntity<Dto> getEntity,
        CancellationToken token = default)
            where Dto : class, new()
        {
            await using var connection = new SqlConnection(GetConnectionString);
            await connection.OpenAsync(token);
            var command = new SqlCommand(nameProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                await using var reader = await command.ExecuteReaderAsync(token);
                return await Convertation(reader, getEntity);
            }
            catch
            {
                await createProcedure();
                await using var reader = await command.ExecuteReaderAsync(token);
                return await Convertation(reader, getEntity);
            }
        }

        private static async Task<Dto> Convertation<Dto>(SqlDataReader reader, ConvertIntoEntity<Dto> getEntity) 
            where Dto : class, new()
        {
            if (reader.HasRows)
            {
                if (await reader.ReadAsync())
                {
                    return getEntity(reader);
                }
            }

            return null;
        }
    }
}
