using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IJwtGenerator
    {
        Task<string> CreateToken(IdentityUser user);
    }
}
