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

            var users = new User[] {
                new User{Name = "User 1",Password= "BADorGu7Ax05j8moM+1etXdQHAsSc5+x",Salt = "-573211833",Email = "user1@gmail.com",IsActive=true,PhoneNumber = "123123"},
                new User{Name = "User 2",Password= "BADorGu7Ax05j8moM+1etXdQHAsSc5+x",Salt = "-573211833",Email = "user2@gmail.com",IsActive=true,PhoneNumber = "123123"}
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            
            var apps = new Application[] {
                new Application{Name = "App 1",Details= "Some Details 1",Url = "https://google.com",Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7e/Free_logos_dribbble_ph.webp/800px-Free_logos_dribbble_ph.webp.png?20210504201705",UserId=1},
                new Application{Name = "App 2",Details= "Some Details 2",Url = "https://outlook.com",Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/FreeTv_Egypt_Logo.png/600px-FreeTv_Egypt_Logo.png?20160229200152",UserId=2}
            };
            foreach (var app in apps)
            {
                context.Applications.Add(app);
            }

            var clsrts = new ClientSecret[] {
                new ClientSecret{ClientID="some.local-1@hexa.sec",Secret="BADorGu7Ax05j8moM+1etXdQHAsSc5+x1",IsActive=true, ApplicationID=1},
                new ClientSecret{ClientID="some.local-2@hexa.sec",Secret="BADorGu7Ax05j8moM+1etXdQHAsSc5+x2",IsActive=true, ApplicationID=2}
            };
            foreach (var clsrt in clsrts)
            {
                context.ClientSecrets.Add(clsrt);
            }
            
            var rds = new RedirectURI[] {
                new RedirectURI{URI="https://localhost-1.com",IsActive=true, ApplicationID=1},
                new RedirectURI{URI="https://localhost-2.com",IsActive=true, ApplicationID=2}
            };

            foreach (var rd in rds)
            {
                context.RedirectURIs.Add(rd);
            }

            //
            context.SaveChanges();



        }

    }
}
