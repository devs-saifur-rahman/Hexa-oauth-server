namespace Hexa.Data.Models.oauth
{
    public class AccessToken
    {
        public int AccessTokenId { get; set; }


        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; }
        public TimeSpan ExpiresIn { get; set; } = new TimeSpan(1,0,0);
        public DateTime CreatedOn { get; set; } = DateTime.Now ;
    }
}
