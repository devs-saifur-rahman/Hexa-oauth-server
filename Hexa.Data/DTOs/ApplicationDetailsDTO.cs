

namespace Hexa.Data.DTOs
{
    public class ApplicationDetailsDTO
    {
        public ApplicationDTO Application;
        public RedirectURIDTO RedirectUrl;
        public List<AppScopeDTO> AssignedScopes;
    }
}
