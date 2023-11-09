using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StateManagement.Models;
using StateManagement.Repository;
namespace StateManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()
    {
        ProductRepository product = new ProductRepository();
        List<Product> products = product.GetProducts();
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }


}
