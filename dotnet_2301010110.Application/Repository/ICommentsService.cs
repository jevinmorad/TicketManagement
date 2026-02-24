using dotnet_23010101171.Core.DTOs.TicketComments;

namespace dotnet_23010101171.Application.Repository;

public interface ICommentsService
{
    Task<TicketCommentResponse> Create(int ticketId, int currentUserId, CreateTicketCommentRequest request);
    Task<IReadOnlyList<TicketCommentResponse>> GetByTicket(int ticketId);
    Task Update(int commentId, int currentUserId, string currentRole, UpdateTicketCommentRequest request);
    Task Delete(int commentId, int currentUserId, string currentRole);
}