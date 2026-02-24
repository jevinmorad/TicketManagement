using dotnet_23010101171.Core.DTOs.Auth;

namespace dotnet_23010101171.Application.Repository;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequests request, CancellationToken cancellationToken = default);
}