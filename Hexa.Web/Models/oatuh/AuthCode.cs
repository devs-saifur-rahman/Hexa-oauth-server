using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexa.Web.Models.oatuh
{
    public class AuthCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthCodeId { get; set; }

        public Guid Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsAuthented { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

    }
}
