using System.Threading.Tasks;

namespace Application.User.Registration
{
    public interface IRegistrationHandler
    {
        Task<User> Registration(RegistrationQuery request);
    }
}
