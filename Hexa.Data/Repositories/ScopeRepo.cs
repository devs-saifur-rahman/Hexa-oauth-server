using Hexa.Data.DB;
using Hexa.Data.Models.oauth;
using Microsoft.EntityFrameworkCore;

namespace Hexa.Data.Repositories
{
    public class ScopeRepo : IScopeRepo
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
         
        public ScopeRepo(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task CreateScopeAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateScopeAsync(Scope scp)
        {
            throw new NotImplementedException();
        }

        public Task DeleteScopeAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteScopeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetScopeDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetScopeDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<List<Scope>> GetScopesAsync()
        {
            return await _dbContext.Scopes.ToListAsync<Scope>();
        }
    }
}
