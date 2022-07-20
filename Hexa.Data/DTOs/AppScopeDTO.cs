namespace Hexa.Data.DTOs
{
    public class AppScopeDTO
    {
        public int ScopeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public int ApplicationId { get; set; }
    }
}
