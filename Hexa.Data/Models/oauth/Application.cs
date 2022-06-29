using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexa.Data.Models.oauth
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationID { get; set; }

        public string Name { get; set; }
        public string Details { get; set; }
        public string Url { get; set; }
        public string Logo { get; set; }

        public int UserId { get; set; }
        public User User { get; }       


        public ICollection<ApplicationScope> ApplicationScopes { get; set; }
    }
}
