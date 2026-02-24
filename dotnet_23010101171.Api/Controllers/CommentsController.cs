using System.Security.Claims;
using dotnet_23010101171.Application.Repository;
using dotnet_23010101171.Core.DTOs.TicketComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_23010101171.Api.Controllers;

[ApiController]
[Authorize]
public sealed class CommentsController(ICommentsService commentsService) : ControllerBase
{
    [HttpPost("tickets/{id}/comments")]
    public async Task<ActionResult<TicketCommentResponse>> Create(int id, [FromBody] CreateTicketCommentRequest request)
    {
        var userId = GetUserId();
        var created = await commentsService.Create(id, userId, request);
        return Ok(created);
    }

    [HttpGet("tickets/{id}/comments")]
    public async Task<ActionResult<IReadOnlyList<TicketCommentResponse>>> GetByTicket(int id)
    {
        var list = await commentsService.GetByTicket(id);
        return Ok(list);
    }

    [HttpPatch("comments/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCommentRequest request)
    {
        var userId = GetUserId();
        var role = GetRole();
        await commentsService.Update(id, userId, role, request);
        return NoContent();
    }

    [HttpDelete("comments/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();
        var role = GetRole();
        await commentsService.Delete(id, userId, role);
        return NoContent();
    }

    private int GetUserId()
    {
        var raw = User.FindFirstValue("UserID");
        return int.TryParse(raw, out var id)
            ? id
            : throw new UnauthorizedAccessException("Missing UserID claim.");
    }

    private string GetRole()
    {
        return User.FindFirstValue(ClaimTypes.Role)
            ?? throw new UnauthorizedAccessException("Missing role claim.");
    }
}