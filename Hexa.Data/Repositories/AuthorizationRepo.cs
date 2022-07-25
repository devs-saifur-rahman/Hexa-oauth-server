using Hexa.Data.DTOs;
using Hexa.Data.DB;
using Microsoft.EntityFrameworkCore;
using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public class AuthorizationRepo : IAuthorizationRepo
    {
        private readonly AppDbContext _dbContext;

        public AuthorizationRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<CodeDTO> GetAuthorizationCode(int id)
        {
            try
            {
                var authResponse = await _dbContext.AuthorizationRequests
                    .Join(_dbContext.Applications, atr => atr.ApplicationID, app => app.ApplicationID, (atr, app) => new
                    {
                        ApplicationID = app.ApplicationID,
                        Name = app.Name,
                        AuthorizationRequestId = atr.AuthorizationRequestId,
                        ApplicationState = atr.ApplicationState,
                        UserId = atr.UserId
                    })
                    .Join(_dbContext.RedirectURIs, a => a.ApplicationID, r => r.ApplicationID, (a, r) => new
                    {

                        Name = a.Name,
                        AuthorizationRequestId = a.AuthorizationRequestId,
                        RedirectUrl = r.URI,
                        ApplicationState = a.ApplicationState,
                        UserId = a.UserId,
                        ApplicationID = a.ApplicationID,
                        code = new Guid().ToString() + a.UserId 
                    })
                    .Where(x => x.AuthorizationRequestId == id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                /// ///+ "." + a.UserId + "-" + a.ApplicationID + "@oauth.v2.hexa.auth.server"

                if (authResponse == null)
                {
                    throw new Exception("Corresponding request not found.");
                }

                AuthCode authCode = new AuthCode
                {
                    Code = authResponse.code,
                    IsActive = true,
                    IsAuthenticated = true,
                    ApplicationID = authResponse.ApplicationID,
                    UserId = authResponse.UserId
                };

                _dbContext.AuthCodes.Add(authCode);

                await SaveChanges();

                CodeDTO resp = new CodeDTO
                {
                    code = authCode.Code,
                    state = authResponse.ApplicationState,
                    success = true,
                    redirect_url = authResponse.RedirectUrl
                };

                return resp;
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        public Task<RepoResponse<Token>> GetAccessToken(TokenRequest tokenRequest)
        {
            RepoResponse<Token> resp;
            try
            {
                var a = (from codes in _dbContext.AuthCodes
                         join apps in _dbContext.Applications on codes.ApplicationID equals apps.ApplicationID
                         where codes.IsActive == true && codes.Code == tokenRequest.code
                         select codes
                    ).AsNoTracking();


                resp = new TokenResponse<Token>
                {
                    success = true,
                    message = "",
                    data = new Token
                    {
                        access_token = "Not Implemented",
                        token_type = "Bearer",
                        scope = "scope1 scope2"
                    }
                };

            }

            catch (Exception ex)
            {
                resp = new RepoResponse<Token>
                {
                    success = false,
                    message = ex.Message
                };
            }

            return Task.FromResult(resp);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public Task<RepoResponse<List<Scope>>> GetApplicationScopes(string clientId, List<string> scopeList)
        {

            var sl = scopeList.ToArray();
            //.Where(s => sl.Contains(s.Tag)).AsEnumerable()
            List<Scope> scopes = (from scp in _dbContext.Scopes
                                  join appscp in _dbContext.ApplicationScopes on scp equals appscp.Scope
                                  join clscrt in _dbContext.ClientSecrets on appscp.ApplicationId equals clscrt.ApplicationID
                                  where clscrt.ClientID == clientId
                                  select scp
                ).ToList<Scope>();

            List<string> extraPermissionAskedFor = sl.Where(s => scopes.All(d => d.Tag != s)).ToList<string>();
            if (extraPermissionAskedFor.Count > 0)
            {
                throw new OverflowException();
            }

            RepoResponse<List<Scope>> resp;

            resp = new RepoResponse<List<Scope>>
            {
                success = true,
                message = "",
                data = scopes
            };
            return Task.FromResult(resp);
        }

        public async Task<AuthorizationRequest> SaveAuthorizationRequest(AuthorizationRequest authRequest)
        {
            await _dbContext.AuthorizationRequests.AddAsync(authRequest);
            await SaveChanges();
            return authRequest;
        }
    }
}
