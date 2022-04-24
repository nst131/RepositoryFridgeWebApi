using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FridgeWebApiBL;
using FridgeWebApiDL;
using FridgeWebApiUI.Common;
using FridgeWebApiUI.HandleMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FridgeWebApiUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "ServiceAccountingUI", Version = "v1" });
                swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = nameof(Authorization),
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddRegistrationDL(Configuration.GetConnectionString("DefaultConnection"));
            services.AddRegistrationBL();
            services.AddRegistrationUI();

            services.AddAutoMapper(typeof(MapperConfigurationUI), typeof(MapperConfigurationBL));

            services.AddMvc().AddNewtonsoftJson();
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();

            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                        ValidateLifetime = true
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy(PolicyService.AllAccess, policy =>
                    policy.RequireRole(Roles.Administrator, Roles.User)
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                option.AddPolicy(PolicyService.User, policy =>
                    policy.RequireRole(Roles.Administrator, Roles.User)
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                option.AddPolicy(PolicyService.Admin, policy =>
                    policy.RequireRole(Roles.Administrator)
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FridgeWebApiUI v1"));


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(s => s.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
