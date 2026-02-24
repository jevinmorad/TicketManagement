using dotnet_23010101171.Core.Entities;

namespace dotnet_23010101171.Core.DTOs.TicketComments;

public class TicketCommentResponse
{
    public int TicketCommentID { get; set; }
    public string Comment { get; set; }
    public Users User { get; set; }
    public DateTime CreatedAt { get; set; }
}
