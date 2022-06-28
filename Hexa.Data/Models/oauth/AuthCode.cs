using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexa.Data.Models.oauth
{
    public class AuthCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthCodeId { get; set; }

        public string Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsAuthenticated { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

    }
}
