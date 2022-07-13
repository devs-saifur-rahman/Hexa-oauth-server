using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;

namespace Hexa.Data.Repositories
{
    public interface IApplicatonRepo
    {
        Task<List<Application>> GetApplicationsAsync();
        Task<Application> GetApplicationById(int id);

        Task CreateApplicationAsync(NewApplicationDTO application);
        Task UpdateApplicationAsync(ApplicationDTO application);
        // Add new url after 
        Task CreateRedirectUrl(RedirectURI redirectURIs);
        //One redirect url at a time
        Task UpdateRedirectURI(RedirectURI redirectURIs);

        Task DeactivateApplication(int id);
        Task DeleteApplicationAsync(int id);

    }
}
