using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MvcWebApp.ViewComponents;

public class LoginForm : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}