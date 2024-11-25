using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebApp.ViewModels;

public class LoginCredential
{

    [Required(ErrorMessage = "请填写账号")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "请填写密码")]
    public required string Password { get; set; }
}