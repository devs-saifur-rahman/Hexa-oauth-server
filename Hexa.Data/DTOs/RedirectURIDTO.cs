using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexa.Data.DTOs
{
    public class RedirectURIDTO
    {
        public int RedirectURIId { get; set; }
        public string URI { get; set; }
        public bool IsActive { get; set; }
        public int ApplicationID { get; set; }
    }
}
