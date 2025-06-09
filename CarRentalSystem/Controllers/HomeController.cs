using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.Models;
using CarRentalSystem.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Controllers;

public class HomeController : Controller
{
    private readonly CarRentalDbContext _context;

    public HomeController(CarRentalDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
        {
            return RedirectToAction("SignIn", "Account");
        }

        var cars = await _context.Cars.Where(c => c.IsAvailable).ToListAsync();
        return View(cars);
    }

    [HttpGet]
    public async Task<IActionResult> SearchForCar(string word)
    {
        var cars = await _context.Cars
            .Where(c => c.IsAvailable &&
                (c.Make.Contains(word) ||
                 c.Model.Contains(word) ||
                 c.Color.Contains(word)))
            .ToListAsync();

        return View("Index", cars);
    }
}
