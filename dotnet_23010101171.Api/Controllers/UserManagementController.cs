using dotnet_23010101171.Application.Repository;
using dotnet_23010101171.Core.DTOs.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_23010101171.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Manager")]
public class UserManagementController(IUserManagementService userManagementService) : ControllerBase
{
    [HttpGet("users")]
    public async Task<ActionResult<IReadOnlyList<ListUsersRequest>>> List() {
        var response = await userManagementService.List();
        return Ok(response);
    }

    [HttpPost("users")]
    public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request)
    {
        try
        {
            var created = await userManagementService.Create(request);
            return Ok(created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}