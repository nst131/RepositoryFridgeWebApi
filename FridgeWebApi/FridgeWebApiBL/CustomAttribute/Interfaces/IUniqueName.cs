using System.Threading.Tasks;
using FridgeWebApiDL.Interfaces;

namespace FridgeWebApiBL.CustomAttribute.Interfaces
{
    public interface IUniqueName
    {
        Task<bool> IsUnique<T>(string name) where T : class, IEntity, IEntityName, new();
        Task<bool> IsUniqueForUpdate<T>(string name, int exceptId) where T : class, IEntity, IEntityName, new();
    }
}