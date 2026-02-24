using dotnet_23010101171.Core.Data;
using dotnet_23010101171.Core.DTOs.Tickets;

namespace dotnet_23010101171.Application.Repository;

public interface ITicketsService
{
    Task<TicketResponse> Create(int currentUserId, CreateTicketRequest request);
    Task<IReadOnlyList<TicketResponse>> Get(int currentUserId, RolesEnum currentRole);
    Task Assign(int ticketId, AssignTicketRequest request);
    Task UpdateStatus(int ticketId, UpdateTicketStatusRequest request, int currentUserId);
    Task Delete(int ticketId);
}