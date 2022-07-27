using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public interface IAuthorizationRepo
    {
       

        Task<RepoResponse<Code>> GetAuthorizationCode(AuthRequest authRequest);

        Task<BearerToken> GetBearerToken(TokenRequest tokenRequest);

        Task SaveChangesAsync();
        Task<RepoResponse<List<Scope>>> GetApplicationScopes(string clientId, List<string> scopeList);
        Task<RepoResponse<Application>> GetApplicationByClientId(string clientId);

    }
}
