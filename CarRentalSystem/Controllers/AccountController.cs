using CarRentalSystem.Contexts;
using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.Models;
namespace CarRentalSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly CarRentalDbContext _context;
        public AccountController(CarRentalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View(user);
                }
                _context.Add(user);
                await _context.SaveChangesAsync();

               
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");

                return RedirectToAction("Index", "Home");
            }
            return View(user);


        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if(user==null || user.Password != password)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View();
            
            }
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");

            return RedirectToAction("Index", "Home");
        }
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
