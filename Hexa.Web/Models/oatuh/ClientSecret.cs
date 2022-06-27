using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Hexa.Web.Models.oatuh
{
    public class ClientSecret
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientSecretID { get; set; }
        

        public int ClientID { get; set; }
        public string Secret { get; set; }
        public string IsActive { get; set; }


        public int ApplicationID { get; set; }
        public Application Application { get; set; }

    }
}
