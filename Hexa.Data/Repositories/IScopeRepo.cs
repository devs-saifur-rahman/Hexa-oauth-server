using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public interface IScopeRepo
    {
        Task CreateScopeAsync(Scope scp);
        Task DeleteScopeAsync(int id);
        Task<List<Scope>> GetScopesAsync();
        Task GetScopeDetailsAsync(int id);

    }
}
