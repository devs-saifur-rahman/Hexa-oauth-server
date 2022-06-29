namespace Hexa.Data.DTOs
{
    public class AuthResponse<T> : ApiResponse<T>
    {

        public T data { get; set; }

    }
}
