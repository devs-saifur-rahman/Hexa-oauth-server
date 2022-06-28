using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexa.Data.Models.oauth
{
    public class RedirectURI
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RedirectURIId { get; set; }
        
        public string URI { get; set; }
        public bool IsActive { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
