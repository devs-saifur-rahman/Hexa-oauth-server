using Hexa.Web.DB;
using Hexa.Web.Models.oatuh;
using Microsoft.AspNetCore.Mvc;

namespace Hexa.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly HexaDbContext _context;

        public AccountController(HexaDbContext context)
        {
            _context = context;
        }

        // GET: Account/Index/5
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,Email,PhoneNumber,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var existUser = _context.Users.Where(query => query.Email.Equals(user.Email)).SingleOrDefault();

                if (existUser == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Login));
                }
                return Problem("User with same email already exists");
            }


            return Problem("Invalid User data");
        }


        // GET: Account/Index/5
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] User user)
        {
            ///TODO:  needs hash checking on password
            var existUser = _context.Users.Where(query => query.Email.Equals(user.Email) && query.Password.Equals(user.Password)).SingleOrDefault();

            if (existUser == null)
            {
                return NotFound();
            }
            else
            {
                //set cookies - authoerize
            }

            return View(existUser);
        }



    }
}
