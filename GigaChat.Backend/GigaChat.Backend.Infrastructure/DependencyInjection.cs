using GigaChat.Backend.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigaChat.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddApplicationUserDbContext(config);

        return services;
    }
    
    private static IServiceCollection AddApplicationUserDbContext(this IServiceCollection services,
        IConfiguration config)
    {
        var identityDbConnectionString = config.GetConnectionString("IdentityDbConnectionString") ??
                                         throw new InvalidOperationException(
                                             "Can't find IdentityDb connection string.");
        services.AddDbContext<ApplicationUserDbContext>(options => options.UseSqlServer(identityDbConnectionString));

        return services;
    }

}