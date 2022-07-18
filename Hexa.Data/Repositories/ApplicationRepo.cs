using AutoMapper;
using Hexa.Data.DB;
using Hexa.Data.DTOs;
using Hexa.Data.Models.oauth;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Hexa.Data.Repositories
{
    public class ApplicationRepo : IApplicationRepo
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public ApplicationRepo(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task CreateApplicationAsync(Application app, string url)
        {
            try
            {

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                app.UserId = int.Parse(userId);


                var k = await _dbContext.Applications.AddAsync(app);

                await SaveChangesAsync();

                var uri = new RedirectURI()
                {
                    IsActive = true,
                    URI = url,
                    ApplicationID = app.ApplicationID
                };

                await _dbContext.RedirectURIs.AddAsync(uri);

                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var s = ex.Message;
            }
        }

        public Task CreateRedirectUrlAsync(RedirectURI redirectURIs)
        {
            throw new NotImplementedException();
        }

        public async Task DeactivateApplicationAsync(int id)
        {

            var app = await _dbContext.Applications.SingleAsync(app => app.ApplicationID == id);

            throw new NotImplementedException();
        }

        public Task DeleteApplicationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationDetailsDTO> GetApplicationById(int applicationId)
        {
            ///TODO:: safety check .. null check . data corruption handle. error handling


            ApplicationDTO application = _mapper.Map<Application, ApplicationDTO>(await _dbContext.Applications.SingleAsync(app => app.ApplicationID == applicationId));

            RedirectURIDTO redirectURI = _mapper.Map<RedirectURI, RedirectURIDTO>(await _dbContext.RedirectURIs.SingleAsync(rdr => rdr.ApplicationID == applicationId));

            var listOfScopes_Old = await (from appScp in _dbContext.ApplicationScopes
                                          join scp in _dbContext.Scopes on appScp.ScopeId equals scp.ScopeId
                                          where appScp.ApplicationId == applicationId
                                          select new AppScopeDTO
                                          {
                                              ScopeId = scp.ScopeId,
                                              Name = scp.Name,
                                              Description = scp.Description,
                                              IsActive = appScp.IsActive,
                                              ApplicationId = applicationId
                                          }).ToListAsync<AppScopeDTO>();


            List<AppScopeDTO> listOfScopes = await _dbContext.Scopes
                .Join(_dbContext.ApplicationScopes, scp => scp.ScopeId, appScp => appScp.ScopeId, (scp, appScp) => new AppScopeDTO
                {
                    ApplicationId = appScp.ApplicationId,
                    ScopeId = scp.ScopeId,
                    Name = scp.Name,
                    Description = scp.Description,
                    IsActive = appScp.IsActive
                }).Where(s => s.ApplicationId == applicationId)
                .ToListAsync();




            ApplicationDetailsDTO applicationDetails = new ApplicationDetailsDTO()
            {
                Application = application,
                RedirectUrl = redirectURI,
                AssignedScopes = listOfScopes
            };


            return applicationDetails;


        }

        public async Task<List<Application>> GetApplicationsAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Application> list = new List<Application>();
            if (userId != null)
            {
                list = await _dbContext.Applications.Where(app => app.UserId == Int32.Parse(userId)).ToListAsync();
            }
            return list;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public Task UpdateApplicationAsync(ApplicationDTO application)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRedirectURIAsync(RedirectURI redirectURIs)
        {
            throw new NotImplementedException();
        }
    }
}
