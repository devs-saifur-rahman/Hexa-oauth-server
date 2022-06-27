using Hexa.Web.DB;
using Hexa.Web.Models.oatuh;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Hexa.Web.Extensions;

namespace Hexa.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly HexaDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public AccountController(HexaDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        // GET: Account/Index/5
        public IActionResult Register()
        {
            return View("Register");
        }

        public string ComputeHash(string toHash, string salt)
        {
            var byteResult = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(toHash), Encoding.UTF8.GetBytes(salt), 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,Email,PhoneNumber,Password,Salt")] User model)
        {
            if (ModelState.IsValid)
            {
                var exisitngUser = _dbContext.Users.Where(query => query.Email.Equals(model.Email)).SingleOrDefault();

                if (exisitngUser == null)
                {
                    var rad = RandomNumberGenerator.Create();
                    byte[] b = new byte[4];
                    rad.GetNonZeroBytes(b);
                    string salt = BitConverter.ToInt32(b).ToString();


                    _dbContext.Add(new User
                    {
                        Name = model.Name,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Password = ComputeHash(model.Password, salt),
                        Salt = salt,
                    });
                    await _dbContext.SaveChangesAsync();
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
        public async Task<IActionResult> Login([Bind("Email,Password")] User model)
        {
            ///TODO:  needs hash checking on password
            var user = _dbContext.Users.Where(query => query.Email.Equals(model.Email)).SingleOrDefault();

            if (user == null)
            {

                return View("Login", model);
            }
            else
            {
                if (ComputeHash(model.Password, user.Salt) != user.Password)
                {
                    return Problem("Password Not Matching");
                    // return View("Login", model);
                }

                //set cookies - authoerize

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("FullName", user.Name),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim("UserId",user.UserId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties { };

                
                await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                _httpContext.HttpContext.Session.Set<int>("User ID", user.UserId);



                return RedirectToAction(actionName: "Index", controllerName: "Applications");


            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(actionName: "Login", controllerName: "Authentication");
        }



    }
}
