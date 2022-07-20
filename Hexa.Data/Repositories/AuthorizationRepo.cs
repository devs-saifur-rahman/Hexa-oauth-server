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


        public Task<RepoResponse<Code>> GetAuthorizationCode(AuthRequest authRequest)
        {
            RepoResponse<Code> resp;

            try
            {
                var a = (from secret in _dbContext.ClientSecrets
                         join application in _dbContext.Applications on secret.ApplicationID equals application.ApplicationID
                         where secret.ClientID == authRequest.client_id
                         select secret).AsNoTracking();



                resp = new AuthResponse<Code>
                {
                    success = true,
                    message = "",
                    data = new Code
                    {
                        code = "Not Implemented",
                        state = authRequest.state
                    }
                };

            }

            catch (Exception ex)
            {
                resp = new RepoResponse<Code>
                {
                    success = false,
                    message = ex.Message
                };
            }

            return Task.FromResult(resp);
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
    }
}
