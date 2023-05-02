using System.Diagnostics;
using Male.Models;
using Microsoft.AspNetCore.Mvc;

namespace Male.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MyDBContext _myDBContext;
    public HomeController(ILogger<HomeController> logger, MyDBContext myDBContext)
    {
        _myDBContext = myDBContext;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var banners = _myDBContext.Banners.ToList();
        ViewBag.banners = banners;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Categories()
    {
        return PartialView("_MenuCategory", _myDBContext.Categories);

    }
    public IActionResult Brands()
    {
        return PartialView("_MenuBrand", _myDBContext.Brands);

    }
}
