using dotnet_23010101171.Core.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_23010101171.Core.Entities;

public class Tickets
{
    [Key]
    public int TicketID { get; set; }

    [Required, StringLength(255)]
    public required string Title { get; set; }

    [Required]
    public required string Description { get; set; }

    public StatusEnum Status { get; set; }
    public PriorityEnum Priority { get; set; }

    [Required]
    public int CreatedBy { get; set; }

    public int AssignedTo { get; set; }

    public int CreatedByUserId { get; set; }

    [ForeignKey(nameof(CreatedByUserId))]
    public Users CreatedByUser { get; set; }

    public int? AssignedToUserId { get; set; }

    [ForeignKey(nameof(AssignedToUserId))]
    public Users? AssignedToUser { get; set; }

    public DateTime CreatedAt { get; set; }
}