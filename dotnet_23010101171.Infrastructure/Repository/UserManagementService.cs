using dotnet_23010101171.Application.Repository;
using dotnet_23010101171.Core.DTOs.UserManagement;
using dotnet_23010101171.Core.Entities;
using dotnet_23010101171.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_23010101171.Infrastructure.Repository;

public sealed class UserManagementService(AppDbContext context) : IUserManagementService
{
    public async Task<IReadOnlyList<ListUsersRequest>> List()
    {
        return await context.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .OrderByDescending(u => u.CreatedAt)
            .Select(u => new ListUsersRequest
            {
                UserID = u.UserID,
                UserName = u.UserName,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<CreateUserResponse> Create(CreateUserRequest request)
    {
        var emailExists = await context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == request.Email);

        if (emailExists)
            throw new InvalidOperationException("Email already exists.");

        var role = await context.Roles
            .FirstOrDefaultAsync(r => r.RoleName == request.Role);

        if (role == null)
            throw new InvalidOperationException("Invalid role.");

        var user = new Users
        {
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password,
            RoleID = role.RoleID,
            Role = role,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new CreateUserResponse
        {
            UserID = user.UserID,
            UserName = user.UserName,
            Role = role,
            CreatedAt = user.CreatedAt
        };
    }
}