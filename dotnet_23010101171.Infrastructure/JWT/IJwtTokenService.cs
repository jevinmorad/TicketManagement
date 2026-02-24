namespace dotnet_23010101171.Infrastructure.JWT;

public interface IJwtTokenService
{
    string CreateToken(int userID, string email, string role);
}