using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cookies.Models;

namespace Cookies.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Check if the language preference is already set
        if (Request.Cookies["Language"] != null)
        {
            // Retrieve the language preference from the cookie
            string language = Request.Cookies["Language"];
            ViewBag.Language = language;
        }

        return View();
    }

    [HttpPost]
    public IActionResult SetLanguage(string language)
    {
        // Create a new cookie named "Language"
        CookieOptions option = new CookieOptions
        {
            // Set the cookie expiration time (e.g., one day)
            Expires = DateTime.Now.AddDays(1)
        };

        Response.Cookies.Append("Language", language, option);

        // Redirect back to the home page
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
