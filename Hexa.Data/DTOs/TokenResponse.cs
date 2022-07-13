namespace Hexa.Data.DTOs
{
    public class TokenResponse<T>  : RepoResponse<T>
    {
        public Token data { get; set; }
    }
}
