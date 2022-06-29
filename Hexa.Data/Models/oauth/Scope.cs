using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexa.Data.Models.oauth
{
    public class Scope
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScopeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }


        public ICollection<ApplicationScope> ApplicationScopes { get; set; }
    }
}
