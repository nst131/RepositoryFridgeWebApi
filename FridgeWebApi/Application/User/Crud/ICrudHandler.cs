using System.Threading.Tasks;

namespace Application.User.Crud
{
    public interface ICrudHandler
    {
        Task<bool> DeleteUser(string email);
    }
}