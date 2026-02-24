using dotnet_23010101171.Application.Repository;
using dotnet_23010101171.Core.DTOs.TicketComments;
using dotnet_23010101171.Core.Entities;
using dotnet_23010101171.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_23010101171.Infrastructure.Repository;

public sealed class CommentsService(AppDbContext context) : ICommentsService
{
    public async Task<TicketCommentResponse> Create(int ticketId, int currentUserId, CreateTicketCommentRequest request)
    {
        var ticketExists = await context.Tickets.AnyAsync(t => t.TicketID == ticketId);
        if (!ticketExists)
            throw new InvalidOperationException("Ticket not found.");

        var comment = new TicketComments
        {
            TicketID = ticketId,
            UserID = currentUserId,
            Commen = request.Comment,
            CreatedAt = DateTime.UtcNow
        };

        context.TicketComments.Add(comment);
        await context.SaveChangesAsync();

        var created = await context.TicketComments
            .AsNoTracking()
            .Include(c => c.User)
            .FirstAsync(c => c.TicketCommentsID == comment.TicketCommentsID);

        return Map(created);
    }

    public async Task<IReadOnlyList<TicketCommentResponse>> GetByTicket(int ticketId)
    {
        var comments = await context.TicketComments
            .AsNoTracking()
            .Include(c => c.User)
            .Where(c => c.TicketID == ticketId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return comments.Select(Map).ToList();
    }

    public async Task Update(int commentId, int currentUserId, string currentRole, UpdateTicketCommentRequest request)
    {
        var comment = await context.TicketComments.FirstOrDefaultAsync(c => c.TicketCommentsID == commentId)
            ?? throw new InvalidOperationException("Comment not found.");

        comment.Commen = request.Comment;
        await context.SaveChangesAsync();
    }

    public async Task Delete(int commentId, int currentUserId, string currentRole)
    {
        var comment = await context.TicketComments.FirstOrDefaultAsync(c => c.TicketCommentsID == commentId)
            ?? throw new InvalidOperationException("Comment not found.");

        context.TicketComments.Remove(comment);
        await context.SaveChangesAsync();
    }

    private static TicketCommentResponse Map(TicketComments c) => new()
    {
        TicketCommentID = c.TicketCommentsID,
        Comment = c.Commen,
        User = c.User,
        CreatedAt = c.CreatedAt
    };
}