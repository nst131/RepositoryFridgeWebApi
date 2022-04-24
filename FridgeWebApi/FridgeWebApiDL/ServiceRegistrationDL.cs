using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Initializer;
using FridgeWebApiDL.Interfaces;
using FridgeWebApiDL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeWebApiDL
{
    public static class ServiceRegistrationDL
    {
        public static void AddRegistrationDL(this IServiceCollection service, string connectionString)
        {
#pragma warning disable CS4014
            CreateDatabase.Initialize(connectionString);
#pragma warning restore CS4014

            service.AddScoped<IRepository<Fridge>, FridgeRepository>(x => new FridgeRepository(connectionString));
            service.AddScoped<IRepository<Products>, ProductsRepository>(x => new ProductsRepository(connectionString));
            service.AddScoped<IRepository<FridgeProducts>, FridgeProductsRepository>(x => new FridgeProductsRepository(connectionString));
            service.AddScoped<IRepository<User>, UserRepository>(x => new UserRepository(connectionString));
            service.AddScoped<IRepository<FridgeModel>, FridgeModelRepository>(x => new FridgeModelRepository(connectionString));
            service.AddScoped<IDbContext, DbContext>(x => new DbContext(x, connectionString));
        }
    }
}
