using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.User.Crud;
using Application.User.Registration;
using AuthApi.BaseModels;
using AuthApi.Models;
using AutoMapper;
using EFData.Models;
using Microsoft.Extensions.Configuration;

namespace AuthApi
{
    public static class AuthHelper
    {
        public static async Task<string> CreateEntity(this ICrudHandler crud, RegistrationQuery request, string token, IMapper mapper, IConfiguration configuration)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(
                configuration.GetSection("AnotherService:FridgeWebApi").Get<string>() + $"/{Roles.User.ToString()}/{CrudOperation.Create}");
            httpRequest.Method = HttpMethod.Post.ToString();
            httpRequest.ContentType = ContentTypeApplication.ApplicationJson;
            httpRequest.Headers.Add($"{nameof(Authorization)}", token);

            await using (var requestStream = httpRequest.GetRequestStream())
            await using (var writer = new StreamWriter(requestStream))
            {
                await writer.WriteAsync(JsonSerializer.Serialize(mapper.Map<RegistrationEntity>(request)));
            }

            try
            {
                using var httpResponse = httpRequest.GetResponse();
                await using var responseStream = httpResponse.GetResponseStream();
                using var reader = new StreamReader(responseStream ?? throw new InvalidOperationException());
                return await reader.ReadToEndAsync();
            }
            catch
            {
                await crud.DeleteUser(request.Email);
                return null;
            }
        }
    }
}
