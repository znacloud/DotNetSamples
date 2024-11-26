using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebApp.ViewModels;

public class LoginCredential
{

    [Required(ErrorMessage = "account_required")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "password_required")]
    public required string Password { get; set; }
}