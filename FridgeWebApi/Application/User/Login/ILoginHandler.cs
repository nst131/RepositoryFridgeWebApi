using System.Threading.Tasks;

namespace Application.User.Login
{
    public interface ILoginHandler
    {
        Task<User> Login(LoginQuery request);
    }
}
