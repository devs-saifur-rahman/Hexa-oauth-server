namespace Hexa.Web.Models.oatuh
{
    public class AccessToken
    {
        public int id { get; set; }
        public int AppId { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; }
        public TimeSpan ExpiresIn { get; set; } = new TimeSpan(1,0,0);
        public DateTime CreatedOn { get; set; } = DateTime.Now ;
    }
}
