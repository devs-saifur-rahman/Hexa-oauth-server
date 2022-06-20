namespace Hexa.Web.Models.oatuh
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Url { get; set; }
        public string Logo { get; set; }

        public List<Application> ApplicationList { get; } = new();
    }
}
