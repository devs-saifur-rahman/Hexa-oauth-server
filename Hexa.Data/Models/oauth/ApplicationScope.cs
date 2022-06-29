using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexa.Data.Models.oauth
{
    public class ApplicationScope
    {
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public int ScopeId { get; set; }
        public Scope Scope { get; set; }

        public bool IsActive { get; set; }


    }
}
