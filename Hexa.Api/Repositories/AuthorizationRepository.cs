using Hexa.Api.DTOs;
using Hexa.Data.DB;
using Microsoft.EntityFrameworkCore;

namespace Hexa.Api.Repositories
{
    public class AuthorizationRepository
    {
        private AppDbContext _dbContext;
        public AuthorizationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ApiResponse GetAuthorizationCode(AuthRequest authRequest)
        {

            ApiResponse resp;
            try
            {
                var a = (from secret in _dbContext.ClientSecrets
                         join application in _dbContext.Applications on secret.ApplicationID equals application.ApplicationId
                         where secret.ClientID == authRequest.client_id
                         select secret).AsNoTracking();



                resp = new AuthResponse
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
                resp = new ApiResponse
                {
                    success = false,
                    message = ex.Message
                };
            }

            return resp;
        }

        public ApiResponse GetAccessToken(TokenRequest tokenRequest)
        {
            ApiResponse resp;
            try
            {
                var a = (from codes in _dbContext.AuthCodes
                         join apps in _dbContext.Applications on codes.ApplicationId equals apps.ApplicationId
                         where codes.IsActive == true && codes.Code == tokenRequest.code
                         select codes
                    ).AsNoTracking();


                resp = new TokenResponse
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
                resp = new ApiResponse
                {
                    success = false,
                    message = ex.Message
                };
            }

            return resp;
        }
    }
}
