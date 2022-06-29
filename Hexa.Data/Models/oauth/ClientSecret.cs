using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Hexa.Data.Models.oauth
{
    public class ClientSecret
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientSecretID { get; set; }
        

        public string ClientID { get; set; }
        public string Secret { get; set; }
        public bool IsActive { get; set; }


        public int ApplicationID { get; set; }
        public Application Application { get; set; }

    }
}
