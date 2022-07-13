using Hexa.Data.DB;
using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hexa.Data.Repositories
{
    public class ApplicationRepo : IApplicationRepo
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationRepo(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task CreateApplicationAsync(NewApplicationDTO application)
        {
            throw new NotImplementedException();
        }

        public Task CreateRedirectUrlAsync(RedirectURI redirectURIs)
        {
            throw new NotImplementedException();
        }

        public Task DeactivateApplicationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteApplicationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Application> GetApplicationById(int applicationId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Application>> GetApplicationsAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Application> list = new List<Application>();
            if (userId != null)
            {
                list = await _dbContext.Applications.Where(app => app.UserId == Int32.Parse(userId)).ToListAsync();
            }
            return list;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public Task UpdateApplicationAsync(ApplicationDTO application)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRedirectURIAsync(RedirectURI redirectURIs)
        {
            throw new NotImplementedException();
        }
    }
}
