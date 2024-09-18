using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace estacionamentoDapper.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
