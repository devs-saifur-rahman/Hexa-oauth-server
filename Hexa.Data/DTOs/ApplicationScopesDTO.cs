namespace Hexa.Data.DTOs
{
    public class ApplicationScopesDTO
    {
        public ApplicationDTO Application { get; set; }

        public IEnumerable<ScopeDTO> Scopes { get; set; }

    }
}
