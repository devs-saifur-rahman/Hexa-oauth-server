using Hexa.Data.DB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Hexa.Web.Extensions;
using Hexa.Data.Models.oauth;
using Hexa.Data.Repositories;
using Hexa.Data.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Web;

namespace Hexa.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationRepo _authRepo;

        public AccountController(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IAuthorizationRepo authRepo)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _authRepo = authRepo;
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
            return View();
        }

        /*

         [HttpPost]

 public IActionResult AddMyStuffBatches(List<MyStuffBatch> batches,[FromQuery] string timestamp, [FromQuery] string apiKey)


         */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            [Bind("Email", "Password")] User model, string returnUrl
            )
        {



            //string queryString = WebUtility.UrlDecode(hReturnUrl);
            //var k = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString);

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


                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                _httpContextAccessor.HttpContext.Session.Set<int>("User ID", user.UserId);


                if (String.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction(actionName: "Index", controllerName: "Applications");
                }
                {

                    return LocalRedirect(returnUrl);

                }



            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(actionName: "Login", controllerName: "Authentication");
        }


        //[HttpGet("oauth/v2/auth")]
        //public IActionResult Authorize()
        [HttpGet("oauth/v2/auth")]
        [Authorize]
        public IActionResult Authorize(string response_type, string client_id, string redirect_uri, string scope, string? state)
        {
            //const string url = "https://localhost:7190/oauth/v2/auth";
            //var param = new Dictionary<string, string>() {
            //    { "response_type","authorization_grant"},
            //    { "client_id","some.local-1@hexa.sec" },
            //    { "redirect_uri","https://saggoogle.com"},
            //    { "scope" , "sc1 sc2"},
            //    {"state","state1"},
            //};

            //var newUrl = new Uri(QueryHelpers.AddQueryString(url, param));


            AuthRequest reqModel = new AuthRequest
            {
                response_type = response_type,
                client_id = client_id,
                redirect_uri = redirect_uri,
                scope = scope,
                state = state
            };

            ApplicationScopesDTO applicationScopesDTO = new ApplicationScopesDTO {
                Application = new ApplicationDTO { 
                    Name = "app 1",
                    Details="app det 1",
                    Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7e/Free_logos_dribbble_ph.webp/800px-Free_logos_dribbble_ph.webp.png?20210504201705",
                    Url="https://google.com"
                },
                Scopes = new List<ScopesDTO> {
                    new ScopesDTO{ Name = "sc1", Description="desc1", Tag="sc1"},
                    new ScopesDTO{ Name = "sc2", Description="desc2", Tag="sc2"}
                }            
            };



            return View("Authorize", applicationScopesDTO);
        }

        [HttpPost("oauth/v2/auth")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authorize(string hasAllowed)
        {

            if(hasAllowed.ToLower() == "allow")
            {

            }
            else
            {

            }

           // await HttpContext.SignOutAsync();
            return RedirectToAction(actionName: "Index", controllerName: "Account");


        }
    }
}
