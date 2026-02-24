using dotnet_23010101171.Application.Repository;
using dotnet_23010101171.Infrastructure.JWT;
using dotnet_23010101171.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_23010101171.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthService, Auth>();
        services.AddScoped<IUserManagementService, UserManagementService>();
        services.AddScoped<ITicketsService, TicketsService>();
        services.AddScoped<ICommentsService, CommentsService>();

        return services;
    }
}
