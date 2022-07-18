using AutoMapper;
using Hexa.Data.DTOs;
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
            CreateMap<Scope,ScopeDTO>();
            CreateMap<ScopeDTO, Scope>();
            CreateMap<ApplicationDTO, Application>();
            CreateMap<Application, ApplicationDTO>();

            CreateMap<RegisterUserDTO, User>();
            CreateMap<User, RegisterUserDTO>();
            CreateMap<LoginUserDTO, User>();
            CreateMap<User, LoginUserDTO>();


            CreateMap<NewApplicationDTO, Application>();
            CreateMap<Application, NewApplicationDTO>();

            CreateMap<RedirectURIDTO, RedirectURI>();
            CreateMap<RedirectURI, RedirectURIDTO>();


        }
    }
}
