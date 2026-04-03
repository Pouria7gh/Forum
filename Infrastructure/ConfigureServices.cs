using Application.Interfaces;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<PasswordService, PasswordServiceImp>();
            services.AddHttpContextAccessor();
            services.AddScoped<UserAccessor, UserAccessorImp>();
            services.AddScoped<SignInService, SignInServiceImp>();

            return services;
        }
    }
}
