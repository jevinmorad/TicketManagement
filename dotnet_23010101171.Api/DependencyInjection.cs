using dotnet_23010101171.Application;
using dotnet_23010101171.Infrastructure;

namespace dotnet_23010101171.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddApplicationDI()
            .AddInfrastructureDI(configuration);
    }
}