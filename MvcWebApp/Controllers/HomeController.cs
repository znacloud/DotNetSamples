using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebApp.Data;
using MvcWebApp.Models;
using MvcWebApp.ViewModels;

namespace MvcWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MvcWebAppContext _context;

    public HomeController(ILogger<HomeController> logger, MvcWebAppContext context)
    {
        _logger = logger;
        _context = context;
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
    public async Task<IActionResult> Riddle()
    {
        var categoryDetails = await _context.RiddleCategories
            .Select(category => new
            {
                Category = category,
                TotalCount = _context.Riddles.Count(r => r.CategoryId == category.Id),
                First3Riddles = _context.Riddles
                    .Where(r => r.CategoryId == category.Id)
                    .OrderBy(r => r.SerialNum)
                    .Take(3)
                    .ToList()
            })
            .OrderBy(c => c.Category.SerialNum)
            .ToListAsync();

        var riddleCategories = categoryDetails
                .Select(c => new RiddleCategoryDetail(c.Category, c.TotalCount, c.First3Riddles))
                .ToList();


        return View("Dashboard/Riddle", riddleCategories);
    }

    [Route("/Dashboard/RiddleList")]
    public async Task<IActionResult> RiddleCategory(string categoryId, int FirstSerialNum = 0, int LastSerialNum = 0)
    {


        List<Riddle>? riddles = null;
        if (FirstSerialNum > 1)
        {
            riddles = _context.Riddles
                .OrderByDescending(r => r.SerialNum)
                .Where(r => r.CategoryId == categoryId && r.SerialNum < FirstSerialNum)
                .Take(20)
                .ToList();
        }
        else
        {
            riddles = _context.Riddles
                .OrderBy(r => r.SerialNum)
                .Where(r => r.CategoryId == categoryId && r.SerialNum > LastSerialNum)
                .Take(20)
                .ToList();
        }

        riddles = riddles?.OrderBy(r => r.SerialNum).ToList();

        var categoryInfo = await _context.RiddleCategories
            .Where(category => category.Id == categoryId)
            .Select(category => new
            {
                Category = category,
                TotalCount = _context.Riddles.Count(r => r.CategoryId == category.Id),

            })
            .ToListAsync();

        var categoryDetails = new RiddleCategoryDetail(categoryInfo[0].Category, categoryInfo[0].TotalCount, riddles ?? []);


        return View("Dashboard/RiddleCategory", categoryDetails);
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

    [HttpPost]
    [AllowAnonymous]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
    }
}
