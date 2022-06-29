using AutoMapper;
using Hexa.Api.DTOs;
using Hexa.Data.Models.oauth;

namespace Hexa.Api.Profiles
{
    public class AuthorizationProfile : Profile
    {
        public AuthorizationProfile()
        {
            // Source -> Target
            CreateMap<AccessToken, Token>();
            CreateMap<AuthRequest, ClientSecret>();
            CreateMap<TokenRequest, ClientSecret>();

        }
    }
}
