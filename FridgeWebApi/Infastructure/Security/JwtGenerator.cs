using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Security
{

    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<IdentityUser> userManager;

        public JwtGenerator(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<string> CreateToken(IdentityUser user)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            var claims = userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)).ToList();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(int.Parse(configuration["JWT:TokenExpiryMinutes"])),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
