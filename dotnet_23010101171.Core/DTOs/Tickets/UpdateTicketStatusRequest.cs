using dotnet_23010101171.Core.Data;

namespace dotnet_23010101171.Core.DTOs.Tickets;

public class UpdateTicketStatusRequest
{
    public StatusEnum Status { get; set; }
}
