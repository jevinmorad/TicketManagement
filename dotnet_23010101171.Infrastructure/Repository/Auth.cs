using dotnet_23010101171.Application.Repository;
using dotnet_23010101171.Core.DTOs.Auth;
using dotnet_23010101171.Infrastructure.Data;
using dotnet_23010101171.Infrastructure.JWT;
using Microsoft.EntityFrameworkCore;

namespace dotnet_23010101171.Infrastructure.Repository;

public class Auth(AppDbContext context, IJwtTokenService jwtTokenService) : IAuthService
{
    public async Task<LoginResponse?> LoginAsync(LoginRequests request, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null)
            return null;

        if (!string.Equals(user.Password, request.Password, StringComparison.Ordinal))
            return null;

        var token = jwtTokenService.CreateToken(
            userID: user.UserID,
            email: user.Email,
            role: user.Role.RoleName.ToString());

        return new LoginResponse { Token = token };
    }
}