using Application.Exceptions;
using Application.User.Crud;
using Application.User.Login;
using Application.User.Registration;
using AutoMapper;
using EFData.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginHandler loginHandler;
        private readonly IRegistrationHandler registrationHandler;
        private readonly IMapper mapper;
        private readonly ICrudHandler crud;
        private readonly IConfiguration configuration;

        public AuthController(
            ILoginHandler loginHandler,
            IRegistrationHandler registrationHandler,
            IMapper mapper,
            ICrudHandler crud,
            IConfiguration configuration)
        {
            this.loginHandler = loginHandler;
            this.registrationHandler = registrationHandler;
            this.mapper = mapper;
            this.crud = crud;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] LoginQuery request)
        {
            if (request is null)
                throw new RestException(HttpStatusCode.BadRequest, $"{nameof(LoginQuery)} is null");

            var user = await loginHandler.Login(request);

            if (user is null)
                throw new RestException(HttpStatusCode.Unauthorized, $"{nameof(user)} is null");

            var jwtToken = $"{JwtBearerDefaults.AuthenticationScheme}" + " " + $"{user.Token}";

            return new JsonResult(new { Response = "Success", Token = jwtToken, user.Email, user.Role });
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<ActionResult<string>> RegistrationAsync([FromBody] RegistrationQuery request)
        {
            if (request is null)
                throw new RestException(HttpStatusCode.BadRequest, $"{nameof(RegistrationQuery)} is null");

            var user = await registrationHandler.Registration(request);

            if (user is null)
                throw new RestException(HttpStatusCode.Unauthorized, $"{nameof(user)} is null");

            var jwtToken = $"{JwtBearerDefaults.AuthenticationScheme}" + " " + $"{user.Token}";

            var responseEntity = await crud.CreateEntity(request, jwtToken, mapper, configuration);

            if (responseEntity is null)
                throw new RestException(HttpStatusCode.Conflict, "Request is not valid, can not create Entity");

            return new JsonResult(new { Response = responseEntity, Token = jwtToken, request.Email, Role = Roles.User.ToString() });
        }
    }
}
