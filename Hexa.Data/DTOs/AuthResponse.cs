namespace Hexa.Data.DTOs
{
    public class AuthResponse<T> : RepoResponse<T>
    {

        public T data { get; set; }

    }
}
