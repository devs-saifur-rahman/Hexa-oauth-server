namespace Hexa.Data.DTOs
{
    public class TokenResponse<T>  : ApiResponse<T>
    {
        public Token data { get; set; }
    }
}
