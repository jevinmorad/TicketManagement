using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_23010101171.Core.Entities;

public class Users
{
    [Key]
    public int UserID { get; set; }
    [Required, StringLength(255)]
    public required string UserName { get; set; }
    [Required, StringLength(255)]
    public required string Email { get; set; }
    [Required, StringLength(255)]
    public required string Password { get; set; }

    [ForeignKey("Roles"), Required]
    public int RoleID { get; set; }
    public required Roles Role { get; set; }

    public DateTime CreatedAt { get; set; }
}
