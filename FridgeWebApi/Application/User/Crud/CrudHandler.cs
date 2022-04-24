using System.Net;
using System.Threading.Tasks;
using Application.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Crud
{
    public class CrudHandler : ICrudHandler
    {
        private readonly UserManager<IdentityUser> userManager;

        public CrudHandler(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> DeleteUser(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new RestException(HttpStatusCode.Conflict, $"Accepted parameter {nameof(email)} is null");

            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                throw new RestException(HttpStatusCode.Conflict, $"{nameof(IdentityUser)} is not found in database");

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new RestException(HttpStatusCode.Conflict, $"Can not delete {nameof(IdentityUser)}");

            return true;
        }
    }
}
