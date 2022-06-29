using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public interface IAuthorizationRepo
    {
       
        Task<ApiResponse> GetAuthorizationCode(AuthRequest authRequest);

        Task<ApiResponse> GetAccessToken(TokenRequest tokenRequest);

        Task SaveChanges();
        Task<ApiResponse> GetAuthorizationCode();


    }
}
