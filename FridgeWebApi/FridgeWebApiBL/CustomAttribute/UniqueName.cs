using FridgeWebApiDL.Context;
using FridgeWebApiDL.Interfaces;
using System.Threading.Tasks;
using FridgeWebApiBL.CustomAttribute.Interfaces;

namespace FridgeWebApiBL.CustomAttribute
{
    public class UniqueName : IUniqueName
    {
        public readonly IDbContext context;

        public UniqueName(IDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> IsUnique<T>(string name) where T : class, IEntity, IEntityName, new()
        {
            var element = await this.context.ExecuteQueryAndRead<T>(this.QueryGetIdByName<T>(name, this.context.GetDatabase), default);
            return element.Count == 0;
        }

        public string QueryGetIdByName<T>(string name, string nameDatabase) where T : class, IEntity, IEntityName, new()
        {
            return $@"
                        Use {nameDatabase}
                        Select Top 1 {nameof(IEntity.Id)} From {typeof(T).Name}
                        Where {nameof(IEntityName.Name)} = '{name}'
                    ";
        }

        public async Task<bool> IsUniqueForUpdate<T>(string name, int exceptId) where T : class, IEntity, IEntityName, new()
        {
            var element = await this.context.ExecuteQueryAndRead<T>(this.QueryGetIdByNameForUpdate<T>(name, exceptId, this.context.GetDatabase), default);
            return element.Count == 0;
        }

        public string QueryGetIdByNameForUpdate<T>(string name,int exceptId, string nameDatabase) where T : class, IEntity, IEntityName, new()
        {
            return $@"
                        Use {nameDatabase}
                        Select Top 1 {nameof(IEntity.Id)} From {typeof(T).Name}
                        Where {nameof(IEntityName.Name)} = '{name}'
                        Except 
                        Select {nameof(IEntity.Id)} From {typeof(T).Name}
                        Where {nameof(IEntity.Id)} = {exceptId}
                    ";
        }
    }
}
