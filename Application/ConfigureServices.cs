using Application.User;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<SignUp>());

            return services;
        }
    }
}
