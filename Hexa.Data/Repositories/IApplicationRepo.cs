using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public interface IApplicationRepo
    {
        Task<List<Application>> GetApplicationsAsync();
        Task<Application> GetApplicationById(int applicationId);

        Task CreateApplicationAsync(Application app, List<String> urls);
        Task UpdateApplicationAsync(ApplicationDTO application);
        // Add new url after 
        Task CreateRedirectUrlAsync(RedirectURI redirectURIs);
        //One redirect url at a time
        Task UpdateRedirectURIAsync(RedirectURI redirectURIs);

        Task DeactivateApplicationAsync(int id);
        Task DeleteApplicationAsync(int id);

        Task SaveChangesAsync();

    }
}
