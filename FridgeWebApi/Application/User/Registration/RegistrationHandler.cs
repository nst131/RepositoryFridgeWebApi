using Application.Exceptions;
using Application.Interfaces;
using EFData.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.User.Registration
{
    public class RegistrationHandler : IRegistrationHandler
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IJwtGenerator jwtGenerator;

        public RegistrationHandler(UserManager<IdentityUser> userManager, IJwtGenerator jwtGenerator)
        {
            this.userManager = userManager;
            this.jwtGenerator = jwtGenerator;
        }

        public async Task<User> Registration(RegistrationQuery request)
        {
            var registrationQueryValidation = new RegistrationQueryValidation();
            var resultRegistrationQueryValidation = await registrationQueryValidation.ValidateAsync(request);

            if (resultRegistrationQueryValidation.IsValid)
                throw new RestException(HttpStatusCode.Unauthorized,
                    resultRegistrationQueryValidation.Errors.ToList().FirstOrDefault()?.ErrorMessage);

            var user = new IdentityUser { UserName = request.Name, Email = request.Email };

            var resultAppendUser = await userManager.CreateAsync(user, request.Password);

            if (!resultAppendUser.Succeeded)
                throw new RestException(HttpStatusCode.BadRequest, $"Can't append {nameof(IdentityUser) }");

            var resultAppendRole = await userManager.AddToRoleAsync(user, Roles.User.ToString());

            if (resultAppendRole.Succeeded)
                return new User()
                {
                    Token = await jwtGenerator.CreateToken(user),
                    Email = request.Email,
                    Role = Roles.User.ToString()
                };

            throw new RestException(HttpStatusCode.BadRequest, $"Can't append {nameof(Roles)}");
        }
    }
}
