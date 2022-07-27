using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public interface IAuthorizationRepo
    {
       

        Task<CodeDTO> GetAuthorizationCode(int id);

        Task<BearerToken> GetBearerToken(TokenRequest tokenRequest);

        Task SaveChangesAsync();
        Task<RepoResponse<List<Scope>>> GetApplicationScopes(string clientId, List<string> scopeList);

        Task<AuthorizationRequest>  SaveAuthorizationRequest(AuthorizationRequest authRequest);
    }
}
