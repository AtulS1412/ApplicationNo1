using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace FirstApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var username = HttpContext.Session.GetString("Username");
        ViewBag.username = username;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult RedirectToSecondApp()
      {
          var token = HttpContext.Session.GetString("JWToken");
          var url = $"http://localhost:5001/Home/login?token={token}";
          return Json(new { url = url });
        //   return Redirect($"http://localhost:5001/Home/login?token={token}");
      }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
