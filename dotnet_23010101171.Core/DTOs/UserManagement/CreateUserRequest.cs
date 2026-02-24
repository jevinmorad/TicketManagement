using dotnet_23010101171.Core.Data;

namespace dotnet_23010101171.Core.DTOs.UserManagement;

public class CreateUserRequest
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required RolesEnum Role { get; set; }
    public required string Password { get; set; }
}
