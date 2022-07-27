namespace Hexa.Data.DTOs
{
    public class AuthorizePromptDTO
    {
        public ApplicationDTO Application;
        public RedirectURIDTO RedirectUrl;
        public List<AppScopeDTO> AssignedScopes;
        public int AuthorizationRequestID;
    }
}
