using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcWebApp.Models;

namespace MvcWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }


    [Route("/Dashboard")]
    public IActionResult Dashboard()
    {
        return View("Dashboard/Index");
    }

    [Route("/Dashboard/Riddle")]
    public IActionResult Riddle()
    {
        return View("Dashboard/Riddle");
    }

      [Route("/Dashboard/Stats")]
    public IActionResult Statistics()
    {
        return View("Dashboard/Stats");
    }

      [Route("/Dashboard/Acl")]
    public IActionResult AccessControl()
    {
        return View("Dashboard/Acl");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
