using System.Threading.Tasks;

namespace FridgeWebApiBL.Common.Interfaces
{
    public interface IValidator<in Entity> where Entity : class
    {
         Task<object> Validate(Entity dto);
    }
}
