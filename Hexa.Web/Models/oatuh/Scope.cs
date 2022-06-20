namespace Hexa.Web.Models.oatuh
{
    public class Scope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }

        public List<Scope> ScopeList { get; set; } = new();
    }
}
