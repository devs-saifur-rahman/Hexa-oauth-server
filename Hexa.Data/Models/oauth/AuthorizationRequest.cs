using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexa.Data.Models.oauth
{
    public class AuthorizationRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorizationRequestId { get; set; }
        public string ClientID { get; set; }
        public string ResponseType { get; set; }
        public string RedirectUri { get; set; }
        public string Scopes { get; set; }
        public string? ApplicationState { get; set; }

        public string? RequestStatus { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RequestTime { get; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int ApplicationID { get; set; }
        public Application Application { get; set; }







    }
}
