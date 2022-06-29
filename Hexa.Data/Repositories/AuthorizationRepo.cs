using Hexa.Data.DTOs;
using Hexa.Data.DB;
using Microsoft.EntityFrameworkCore;

namespace Hexa.Data.Repositories
{
    public class AuthorizationRepo : IAuthorizationRepo
    {
        private readonly AppDbContext _dbContext;

        public AuthorizationRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task<ApiResponse> GetAuthorizationCode(AuthRequest authRequest)
        {

            ApiResponse resp;
            try
            {
                var a = (from secret in _dbContext.ClientSecrets
                         join application in _dbContext.Applications on secret.ApplicationID equals application.ApplicationID
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

            return Task.FromResult(resp);
        }

        public Task<ApiResponse> GetAccessToken(TokenRequest tokenRequest)
        {
            ApiResponse resp;
            try
            {
                var a = (from codes in _dbContext.AuthCodes
                         join apps in _dbContext.Applications on codes.ApplicationID equals apps.ApplicationID
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
            
            return Task.FromResult(resp);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ApiResponse> GetAuthorizationCode()
        {
            ApiResponse resp;
            //try
            //{
            //    var a = (from secret in _dbContext.ClientSecrets
            //             join application in _dbContext.Applications on secret.ApplicationID equals application.ApplicationID
            //             where secret.ClientID == authRequest.client_id
            //             select secret).AsNoTracking();



            //resp = new AuthResponse
            //{
            //    success = true,
            //    message = "",
            //    data = new Code
            //    {
            //        code = "Not Implemented",
            //        state = authRequest.state
            //    }
            //};

            //}

            //catch (Exception ex)
            //{
            //resp = new ApiResponse
            //    {
            //        success = false,
            //        message = "Not Implmented ye"
            //    };
            // }

            resp = new AuthResponse
            {
                success = true,
                message = "",
                data = new Code
                {
                    code = "Not Implemented AuthResponse",
                    state = "No State"
                }
            };

            return resp; 
        }
    }
}
