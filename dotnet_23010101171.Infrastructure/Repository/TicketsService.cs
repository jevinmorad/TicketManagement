using dotnet_23010101171.Application.Repository;
using dotnet_23010101171.Core.Data;
using dotnet_23010101171.Core.DTOs.Tickets;
using dotnet_23010101171.Core.Entities;
using dotnet_23010101171.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_23010101171.Infrastructure.Repository;

public sealed class TicketsService(AppDbContext context) : ITicketsService
{
    private static readonly Dictionary<StatusEnum, StatusEnum> AllowedTransitions = new()
    {
        { StatusEnum.Open, StatusEnum.InProgress },
        { StatusEnum.InProgress, StatusEnum.Resolved },
        { StatusEnum.Resolved, StatusEnum.Closed }
    };

    public async Task<TicketResponse> Create(int currentUserId, CreateTicketRequest request)
    {
        var ticket = new Tickets
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = StatusEnum.Open,
            CreatedByUserId = currentUserId,
            AssignedToUserId = null,
            CreatedAt = DateTime.UtcNow
        };

        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();

        var created = await context.Tickets
            .AsNoTracking()
            .Include(t => t.CreatedByUser)
            .Include(t => t.AssignedToUser)
            .FirstAsync(t => t.TicketID == ticket.TicketID);

        return Map(created);
    }

    public async Task<IReadOnlyList<TicketResponse>> Get(int currentUserId, RolesEnum currentRole)
    {
        IQueryable<Tickets> query = context.Tickets
            .AsNoTracking()
            .Include(t => t.CreatedByUser)
            .Include(t => t.AssignedToUser);

        query = currentRole switch
        {
            RolesEnum.Manager => query,
            RolesEnum.Support => query.Where(t => t.AssignedToUserId == currentUserId),
            RolesEnum.User => query.Where(t => t.CreatedByUserId == currentUserId),
            _ => query.Where(_ => false)
        };

        var tickets = await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return tickets.Select(Map).ToList();
    }

    public async Task Assign(int ticketId, AssignTicketRequest request)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.TicketID == ticketId)
            ?? throw new InvalidOperationException("Ticket not found.");

        ticket.AssignedToUserId = request.UserID;
        await context.SaveChangesAsync();
    }

    public async Task UpdateStatus(int ticketId, UpdateTicketStatusRequest request, int currentUserId)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.TicketID == ticketId)
            ?? throw new InvalidOperationException("Ticket not found.");

        if (ticket.Status == request.Status)
            return;

        if (!IsValidTransition(ticket.Status, request.Status))
            throw new InvalidOperationException(
                $"Invalid status transition: {ticket.Status} → {request.Status}.");

        context.TicketStatusLogs.Add(new TicketStatusLogs
        {
            TicketID = ticket.TicketID,
            OldStatus = ticket.Status,
            NewStatus = request.Status,
            ChangedByUserID = currentUserId,
            CreatedAt = DateTime.UtcNow
        });

        ticket.Status = request.Status;
        await context.SaveChangesAsync();
    }

    public async Task Delete(int ticketId)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.TicketID == ticketId)
            ?? throw new InvalidOperationException("Ticket not found.");

        context.Tickets.Remove(ticket);
        await context.SaveChangesAsync();
    }

    private static bool IsValidTransition(StatusEnum current, StatusEnum next)
        => AllowedTransitions.TryGetValue(current, out var allowed) && allowed == next;

    private static TicketResponse Map(Tickets t) => new()
    {
        TicketID = t.TicketID,
        Title = t.Title,
        Status = t.Status.ToString(),
        Priority = t.Priority.ToString(),
        CreatedByUser = t.CreatedByUser,
        AssignedTo = t.AssignedToUser,
        CreatedAt = t.CreatedAt
    };
}