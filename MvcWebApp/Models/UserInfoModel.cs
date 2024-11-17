using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebApp.Models;
[Table("User")]
public class UserInfoModel
{
    [Key]
    [Required]
    [Column(TypeName = "varchar(255)")]
    public required string Id { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(64)")]
    public required string Username { get; set; }

    [Required]
    [Column(TypeName = "varchar(64)")]
    public required string Email { get; set; }

    [Required]
    [Column(TypeName = "varchar(64)")]
    public required string NickName { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(255)")]
    public required string AvatarUrl { get; set; }

    [Column(TypeName = "text")]
    public string[]? Roles { get; set; }
    
  
    [DefaultValue(0)]
    [Column(TypeName = "tinyint(8)")]
    public byte Gender { get; set; }

    [Column(TypeName = "varchar(64)")]
    public string? Language { get; set; }

    [Column(TypeName = "varchar(64)")]
    public string? City { get; set; }

    [Column(TypeName = "varchar(64)")]
    public string? Province { get; set; }

    [Column(TypeName = "varchar(64)")]
    public string? Country { get; set; }
}

