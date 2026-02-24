using dotnet_23010101171.Core.Entities;

namespace dotnet_23010101171.Core.DTOs.UserManagement;

public class CreateUserResponse
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public Roles Role { get; set; }
    public DateTime CreatedAt { get; set; }
}
