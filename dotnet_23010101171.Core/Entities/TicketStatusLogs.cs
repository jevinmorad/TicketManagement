using dotnet_23010101171.Core.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_23010101171.Core.Entities;

public class TicketStatusLogs
{
    [Key]
    public int TicketStatusLogID { get; set; }
    [Required]
    public int TicketID { get; set; }
    [ForeignKey("TicketID")]
    public Tickets? Ticket { get; set; }
    [Required]
    public StatusEnum OldStatus { get; set; }
    [Required]
    public StatusEnum NewStatus { get; set; }
    [Required]
    public int ChangedByUserID { get; set; }
    [ForeignKey("UserID")]
    public Users ChangedByUser { get; set; }
    public DateTime CreatedAt { get; set; }
}