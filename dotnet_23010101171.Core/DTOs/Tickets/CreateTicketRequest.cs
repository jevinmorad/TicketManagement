using dotnet_23010101171.Core.Data;

namespace dotnet_23010101171.Core.DTOs.Tickets;

public class CreateTicketRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public PriorityEnum Priority { get; set; }
}
