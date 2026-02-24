using dotnet_23010101171.Core.Entities;

namespace dotnet_23010101171.Core.DTOs.Tickets;

public class TicketResponse
{
    public int TicketID { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public Users CreatedByUser { get; set; }
    public Users AssignedTo { get; set; }
    public DateTime CreatedAt { get; set; }
}
