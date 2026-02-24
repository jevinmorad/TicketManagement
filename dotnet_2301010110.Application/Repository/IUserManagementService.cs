using dotnet_23010101171.Core.DTOs.UserManagement;

namespace dotnet_23010101171.Application.Repository;

public interface IUserManagementService
{
    Task<IReadOnlyList<ListUsersRequest>> List();
    Task<CreateUserResponse> Create(CreateUserRequest request);
}