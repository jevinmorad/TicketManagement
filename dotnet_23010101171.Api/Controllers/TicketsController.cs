using System.Security.Claims;
using dotnet_23010101171.Application.Repository;
using dotnet_23010101171.Core.Data;
using dotnet_23010101171.Core.DTOs.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_23010101171.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class TicketsController(ITicketsService ticketsService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Manager,User")]
    public async Task<ActionResult<TicketResponse>> Create([FromBody] CreateTicketRequest request)
    {
        var userId = GetUserId();
        var created = await ticketsService.Create(userId, request);
        return Ok(created);
    }

    [HttpGet]
    [Authorize(Roles = "Manager,Support,User")]
    public async Task<ActionResult<IReadOnlyList<TicketResponse>>> Get()
    {
        var userId = GetUserId();
        var role = GetRoleEnum();
        var list = await ticketsService.Get(userId, role);
        return Ok(list);
    }

    [HttpPatch("{id}/assign")]
    [Authorize(Roles = "Manager,Support")]
    public async Task<IActionResult> Assign(int id, [FromBody] AssignTicketRequest request)
    {
        await ticketsService.Assign(id, request);
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Manager,Support")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTicketStatusRequest request)
    {
        var userId = GetUserId();
        await ticketsService.UpdateStatus(id, request, userId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager,Support")]
    public async Task<IActionResult> Delete(int id)
    {
        await ticketsService.Delete(id);
        return NoContent();
    }

    private int GetUserId()
    {
        var raw = User.FindFirstValue("UserID");
        return int.TryParse(raw, out var id)
            ? id
            : throw new UnauthorizedAccessException("Missing UserID claim.");
    }

    private RolesEnum GetRoleEnum()
    {
        var role = User.FindFirstValue(ClaimTypes.Role);
        return Enum.TryParse<RolesEnum>(role, ignoreCase: true, out var parsed)
            ? parsed
            : throw new UnauthorizedAccessException("Missing role claim.");
    }
}