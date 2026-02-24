using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_23010101171.Core.Entities;

public class TicketComments
{
    [Key]
    public int TicketCommentsID { get; set; }
    public int TicketID { get; set; }
    [ForeignKey("TicketID")]
    public Tickets? Ticket { get; set; }
    [Required]
    public int UserID { get; set; }
    [ForeignKey("UserID")]
    public Users User { get; set; }
    public required string Commen { get; set; }
    public DateTime CreatedAt { get; set; }
}