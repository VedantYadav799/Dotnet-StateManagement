using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Cache.Models;


namespace Cache.Controllers;

public class HomeController : Controller
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public IActionResult Index()
    {
       // Try to get the list of countries from the cache
        if (!_cache.TryGetValue("Countries", out List<string> countries))
        {
            Console.WriteLine("getting and setting data in cache");
            // If not in the cache, simulate fetching from a data source (e.g., a database)
            countries = GetCountries();

            // Cache the list of countries for 5 minutes
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
            };

            _cache.Set("Countries", countries, cacheEntryOptions);
        }

        Console.WriteLine("from cache");
        ViewBag.Countries = countries;

        return View();
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

      private List<string> GetCountries()
    {
        return new List<string>
        {
            "United States",
            "Canada",
            "United Kingdom",
            "Germany",
            "France",
            "Australia",
            "Japan",
            "India",
            "Brazil",
            "South Africa"
        };
    }
}
