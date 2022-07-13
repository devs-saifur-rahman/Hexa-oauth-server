namespace Hexa.Data.DTOs
{
    public class RepoResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T? data { get; set; }

    }
}
