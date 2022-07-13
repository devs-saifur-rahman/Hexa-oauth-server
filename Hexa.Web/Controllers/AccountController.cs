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
using AutoMapper;

namespace Hexa.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationRepo _authRepo;
        private readonly IMapper _mapper;

        public AccountController(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IAuthorizationRepo authRepo, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _authRepo = authRepo;
            _mapper = mapper;
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
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var exisitngUser = _dbContext.Users.Where(query => query.Email.Equals(model.Email)).SingleOrDefault();

                if (exisitngUser == null)
                {

                    var rad = RandomNumberGenerator.Create();
                    byte[] b = new byte[4];
                    rad.GetNonZeroBytes(b);

                    User user = _mapper.Map<RegisterUserDTO, User>(model);
                    user.Salt = BitConverter.ToInt32(b).ToString();
                    user.Password = ComputeHash(model.Password, user.Salt);

                    _dbContext.Add(user);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO model, string returnUrl)
        {   var user = _dbContext.Users.Where(query => query.Email.Equals(model.Email)).SingleOrDefault();



            if (user == null)
            {

                return View("Login", model);
            }
            else
            {
                if (ComputeHash(model.Password, user.Salt) != user.Password)
                {
                    //return Problem("Password Not Matching");
                    return View("Login", model);
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
                _httpContextAccessor.HttpContext.Session.Set("claim", claims);


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
        public async Task<IActionResult> Authorize(string response_type, string client_id, string redirect_uri, string scope, string? state)
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

            //        //sample call

            /**
            https://localhost:7190/
            oauth/v2/auth
            ?
            response_type=response_type
            client_id=some.local-1@hexa.sec
            redirect_uri = 'https://saggoogle.com'
            scope='sc1 sc2'
            state='state1'	
                to generate this 
            //https://localhost:7190/oauth/v2/auth?response_type=authorization_grant&client_id=some.local-1@hexa.sec&redirect_uri=https%3A%2F%2Fsaggoogle.com&scope=sc1 sc2&state=state1
            */

            AuthRequest reqModel = new AuthRequest
            {
                response_type = response_type,
                client_id = client_id,
                redirect_uri = redirect_uri,
                scope = scope,
                state = state
            };


            var application = await _authRepo.GetApplicationByClientId(client_id);

            var scopesList = await _authRepo.GetApplicationScopes(client_id, scope.ToLower().Split(' ').ToList());

            ApplicationDTO appDTO = _mapper.Map<Application, ApplicationDTO>(application.data);
            List<ScopeDTO> scopesDTO = _mapper.Map<List<Scope>, List<ScopeDTO>>(scopesList.data);


            ApplicationScopesDTO applicationScopesDTO = new ApplicationScopesDTO
            {
                Application = appDTO,
                Scopes = scopesDTO
            };



            return View("Authorize", applicationScopesDTO);
        }

        [HttpPost("oauth/v2/auth")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authorize(string hasAllowed)
        {

            if (hasAllowed.ToLower() == "allow")
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
