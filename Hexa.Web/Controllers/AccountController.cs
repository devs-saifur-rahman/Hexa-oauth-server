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
using Newtonsoft.Json;
using System.Collections.Specialized;
using Microsoft.Extensions.Primitives;

namespace Hexa.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationRepo _authRepo;
        private readonly IAccountRepo _accRepo;
        private readonly IApplicationRepo _appRepo;
        private readonly IMapper _mapper;

        public AccountController(AppDbContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationRepo authRepo,
            IAccountRepo accRepo,
            IApplicationRepo appRepo,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _authRepo = authRepo;
            _accRepo = accRepo;
            _appRepo = appRepo;
            _mapper = mapper;
        }


        #region Registration

        public IActionResult Register()
        {
            return View("Register");
        }


        /// <summary>
        /// Registration form submission
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Redirects to Login page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _accRepo.CheckIfUserExistByMail(model.Email);

                if (!userExists.data)
                {
                    User user = _mapper.Map<RegisterUserDTO, User>(model);
                    await _accRepo.Register(user);

                    return RedirectToAction(nameof(Login));
                }
                ///TODO : Add sweetalert pop or some proper message in register page
                ViewBag.ErrorMessage = "User with same email already exists";
                return View("Register", model);
            }
            ViewBag.ErrorMessage = "Invalid User data";
            return View("Register", model);
        }

        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO model, string returnUrl)
        {
            //var user = _dbContext.Users.Where(query => query.Email.Equals(model.Email)).SingleOrDefault();
            var userData = await _accRepo.Login(model);

            if (!userData.success)
            {
                ViewBag.ErrorMessage = userData.message;
                return View("Login", model);
            }
            else
            {
                var user = userData.data;

                //set cookies - authoerize

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                _httpContextAccessor.HttpContext.Session.Set<LoginUserDTO>("userInfo", model);

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                if (String.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction(actionName: "Index", controllerName: "Applications");
                }
                else
                {
                    return LocalRedirect(returnUrl);
                }
            }
        }

        #endregion

        #region Signout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("Login");
        }
        #endregion

        #region authorization

        //[HttpGet("oauth/v2/auth")]
        //public IActionResult Authorize()
        [HttpGet("oauth/v2/authorize")]
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
            //https://localhost:7190/oauth/v2/authorize?response_type=authorization_grant&client_id=some.local-1@hexa.sec&redirect_uri=https%3A%2F%2Fsaggoogle.com&scope=Scope_Name_1%20Scope_Name_2&state=state1
            */

            AuthRequest reqModel = new AuthRequest
            {
                response_type = response_type,
                client_id = client_id,
                redirect_uri = redirect_uri,
                scope = scope,
                state = state
            };


            //TODO - store the request in DB to save the state and have a request id

            ApplicationDetailsDTO application = await _appRepo.GetApplicationByClientId(client_id);

            if (application.Application.ApplicationID < 1)
            {
                ///TODO : forward to Error page
                throw new Exception("No Application found");
            }

            //store the request in db 


            AuthorizationRequest authRequest = await _authRepo.SaveAuthorizationRequest(new AuthorizationRequest
            {
                ClientID = client_id,
                UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                ApplicationID = application.Application.ApplicationID,
                ResponseType = response_type,
                RedirectUri = redirect_uri,
                Scopes = scope,
                ApplicationState = state,
                RequestStatus = "pending"
            });

            // TODO - safer way to store and get authRequest.ID so that front end edit cannot change it 

            //  _httpContextAccessor.HttpContext.Session.Set<string>("AuthorizationRequestId", JsonContent.Create(authRequest).ToString());


            List<string> reqScopes = scope.Split(' ').ToList();

            var scopeMatched = application.AssignedScopes.Select(x => x.Name).ToList().Intersect(reqScopes).Count() == reqScopes.Count();

            if (!scopeMatched)
            {
                ///TODO : forward to Error page
                throw new Exception("Scope does not match");
            }

            // if all ok , show the auth prompt 
            AuthorizePromptDTO authorizePromptDTO = new AuthorizePromptDTO
            {
                Application = application.Application,
                RedirectUrl = application.RedirectUrl,
                AssignedScopes = application.AssignedScopes,
                AuthorizationRequestID = authRequest.AuthorizationRequestId
            };

            return View("Authorize", authorizePromptDTO);
        }

        [HttpPost("oauth/v2/authorize")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptAuthorize(string authorizationRequestID, string hasAllowed)
        {


            if (hasAllowed.ToLower() == "allow")
            {
                CodeDTO code = await _authRepo.GetAuthorizationCode(int.Parse(authorizationRequestID));

                if (code.success)
                {


                    Uri uri = new Uri(code.redirect_url);
                    Dictionary<string, StringValues> parsedQueryString = QueryHelpers.ParseQuery(uri.Query);

                    parsedQueryString.Add("code", code.code);
                    parsedQueryString.Add("state", code.state);
                    string queryString = QueryString.Create(parsedQueryString).ToString();
                    string redirectionUrl = uri.Scheme + "://" + uri.Host + uri.AbsolutePath + queryString;

                    return RedirectPermanent(redirectionUrl);
                }
                else
                {
                    return RedirectToAction(actionName: "Index", controllerName: "Account");
                }
            }
            else
            {
                //          var k = _auth

                return RedirectToAction(actionName: "Index", controllerName: "Account");
            }
        }

        [HttpGet("oauth/v2/token")]
        public async Task<IActionResult> GenerateAccessToken( string grant_type, string code, string redirect_uri, string client_id, string client_secret)
        {

            TokenRequest tokenRequest = new TokenRequest { 
                grant_type = grant_type,
                code = code,
                redirect_uri   = redirect_uri,
                client_id = client_id,
                client_secret = client_secret
            };
            /*
              https://localhost:7190/
         oauth/v2/token/access?
            grant_type=authorization_code&  
            code=AUTH_CODE_HERE&
            redirect_uri=REDIRECT_URI&
            client_id=CLIENT_ID&
            client_secret=CLIENT_SECRET
               https://localhost:7190/oauth/v2/token?grant_type=authorization_code&code=00000000-0000-0000-0000-0000000000003&redirect_uri='https://saggoogle.com'&client_id=some.local-1@hexa.sec&client_secret=BADorGu7Ax05j8moM+1etXdQHAsSc5+x1
             */

            var tokenResponse = await _authRepo.GetBearerToken(tokenRequest);
            return Ok(tokenResponse);
        }

        [HttpGet("oauth/v2/refreshtoken")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefreshToken(string hasAllowed)
        {


            return RedirectToAction(actionName: "Index", controllerName: "Account");
        }

        #endregion

    }
}
