namespace Hexa.Data.DTOs
{
    public class CodeDTO
    {
        public string redirect_url { get; set; }
        public string? code { get; set; }
        public string? state { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
    }
}
