using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public interface IAuthorizationRepo
    {
       

        Task<RepoResponse<Code>> GetAuthorizationCode(int id);

        Task<RepoResponse<Token>> GetAccessToken(TokenRequest tokenRequest);

        Task SaveChanges();
        Task<RepoResponse<List<Scope>>> GetApplicationScopes(string clientId, List<string> scopeList);

        Task<AuthorizationRequest>  SaveAuthorizationRequest(AuthorizationRequest authRequest);
    }
}
