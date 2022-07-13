using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public interface IAccountRepo
    {
        Task<RepoResponse<bool>> Register(User model);
        Task<RepoResponse<User>> Login(LoginUserDTO model);
        Task<RepoResponse<bool>> CheckIfUserExistByMail(string userEmail);

        Task SaveChanges();
    }
}
