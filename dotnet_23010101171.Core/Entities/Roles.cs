using dotnet_23010101171.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace dotnet_23010101171.Core.Entities;

public class Roles
{
    [Key]
    public int RoleID { get; set; }
    [Required]
    public required RolesEnum RoleName { get; set; }
}