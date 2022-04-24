using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Login
{
    public class LoginHandler : ILoginHandler
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IJwtGenerator jwtGenerator;

        public LoginHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtGenerator jwtGenerator)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtGenerator = jwtGenerator;
        }

        public async Task<User> Login(LoginQuery request)
        {
            var loginQueryValidation = new LoginQueryValidation();
            var resultLoginQueryValidation = await loginQueryValidation.ValidateAsync(request);

            if (resultLoginQueryValidation.IsValid)
                throw new RestException(HttpStatusCode.Unauthorized,
                    resultLoginQueryValidation.Errors.ToList().FirstOrDefault()?.ErrorMessage);

            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new RestException(HttpStatusCode.Unauthorized, $"{nameof(User)} is not exist");

            var passwordVerification = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (!passwordVerification.Succeeded)
                throw new RestException(HttpStatusCode.Unauthorized, $"{nameof(User)} is not pass verification of password");

            var userRoles = await userManager.GetRolesAsync(user);

            if (userRoles.Any())
            {
                return new User
                {
                    Token = await jwtGenerator.CreateToken(user),
                    Email = request.Email,
                    Role = userRoles.FirstOrDefault()
                };
            }

            throw new RestException(HttpStatusCode.Unauthorized, $"{nameof(User)} is not pass verification of role");
        }
    }
}
