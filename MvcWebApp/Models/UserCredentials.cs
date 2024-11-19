using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebApp.Models;

public class UserCredentials
{

    [Key]
    [Required]
    [Column(TypeName = "varchar(255)")]
    public required string UserId { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public required string Password { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? PrivacyCode { get; set; }
}
