using FridgeWebApiUI.Common;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeWebApiUI
{
    public static class ServiceRegistrationUI
    {
        public static void AddRegistrationUI(this IServiceCollection service)
        {
            service.AddScoped<IFileHelper, FileHelper>();
        }
    }
}
