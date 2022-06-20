namespace Hexa.Web.Models.oatuh
{
    public class AuthCode
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AppId { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsAuthented { get; set; }
    }
}
