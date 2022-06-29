using Hexa.Api.DTOs;
namespace Hexa.Api.Repositories
{
    public interface IAuthorizationRepo
    {
       
        Task<ApiResponse> GetAuthorizationCode(AuthRequest authRequest);

        Task<ApiResponse> GetAccessToken(TokenRequest tokenRequest);

        Task SaveChanges();
        Task<ApiResponse> GetAuthorizationCode();
    }
}
