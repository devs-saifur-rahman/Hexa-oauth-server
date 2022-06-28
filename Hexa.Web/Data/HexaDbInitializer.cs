using Hexa.Data.Models.oauth;
using Hexa.Data.DB;

namespace Hexa.Web.Data
{
    public static class HexaDbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.GrantTypes.Any())
            {
                return;
            }

            var grantTypes = new GrantType[] { 
                      new GrantType{Name=GrantName.AUTHORIZATION,Description="Authorization grant"},
                      new GrantType{Name=GrantName.PASSWORD,Description="Username Password grant type"},
                      new GrantType{Name=GrantName.PKCE,Description="Proof Key Challenge E type"},
                      new GrantType{Name=GrantName.IMPLICITE,Description="implicit grant"},
                      new GrantType{Name=GrantName.CLIENT_CREDENTIAL,Description="client crednetial grant"}
            };
            foreach (var grantType in grantTypes)
            {
                context.GrantTypes.Add(grantType);
            }

            context.SaveChanges();



        }

    }
}
