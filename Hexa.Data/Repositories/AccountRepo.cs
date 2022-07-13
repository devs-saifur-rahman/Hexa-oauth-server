using Hexa.Data.DB;
using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Hexa.Data.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext _dbContext;

        public AccountRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RepoResponse<bool>> CheckIfUserExistByMail(string userEmail)
        {

            RepoResponse<bool> resp;

            try
            {
                var userExists = await _dbContext.Users.Where(query => query.Email.Equals(userEmail)).FirstOrDefaultAsync() == null;

                resp = new RepoResponse<bool>
                {
                    success = true,
                    message = "",
                    data = userExists
                };
            }
            catch (Exception ex)
            {
                resp = new RepoResponse<bool>
                {
                    success = false,
                    message = ex.Message
                };
            }
            return resp;

        }

        public async Task<RepoResponse<User>> Login(LoginUserDTO model)
        {
            RepoResponse<User> resp;
            try
            {
                var user = await _dbContext.Users.Where(query => query.Email.Equals(model.Email)).FirstOrDefaultAsync();
                if(user == null)
                {
                    throw new Exception("User does not exist");
                }
                if (Utill.Helper.ComputeHash(model.Password, user.Salt) != user.Password)
                {
                    throw new Exception("Password Not Matching");
                }
                if (!user.IsActive)
                {
                    throw new Exception("User is not active");
                }
                resp = new RepoResponse<User>
                {
                    success = true,
                    message = "",
                    data = user
                };

            }
            catch (Exception ex)
            {
                resp = new RepoResponse<User>
                {
                    success = false,
                    message = ex.Message
                };
            }
            return resp;
        }

        public async Task<RepoResponse<bool>> Register(User model)
        {
            RepoResponse<bool> resp;
            try
            {
                var rad = RandomNumberGenerator.Create();
                byte[] b = new byte[4];
                rad.GetNonZeroBytes(b);

                model.Salt = BitConverter.ToInt32(b).ToString();
                model.Password = Utill.Helper.ComputeHash(model.Password, model.Salt);

                _dbContext.Add(model);
                await SaveChanges();

                resp = new RepoResponse<bool>
                {
                    success = true,
                    message = "",
                    data = true
                };
            }
            catch (Exception ex)
            {
                resp = new RepoResponse<bool>
                {
                    success = false,
                    message = ex.Message
                };
            }
            return resp;
        }

        public async Task SaveChanges()
        {
            var k = await _dbContext.SaveChangesAsync();
            var r = 12;
        }
    }
}