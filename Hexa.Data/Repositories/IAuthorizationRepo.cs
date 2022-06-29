using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public interface IAuthorizationRepo
    {
       

        Task<ApiResponse<Code>> GetAuthorizationCode(AuthRequest authRequest);

        Task<ApiResponse<Token>> GetAccessToken(TokenRequest tokenRequest);

        Task SaveChanges();
        Task<ApiResponse<List<Scope>>> GetApplicationScopes(string clientId, List<string> scopeList);
        Task<ApiResponse<Application>> GetApplicationByClientId(string clientId);

    }
}
