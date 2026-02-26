using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Providers;

namespace Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("Default"));
        });

        return services;
    }
}
