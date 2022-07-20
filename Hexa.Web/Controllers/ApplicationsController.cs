using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hexa.Data.DB;
using Hexa.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Hexa.Data.Models.oauth;
using System.Security.Claims;
using Hexa.Data.Repositories;
using Hexa.Data.DTOs;
using AutoMapper;

namespace Hexa.Web.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationRepo _applicationRepo;

        public ApplicationsController(AppDbContext dbContext, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IApplicationRepo applicationRepo)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _applicationRepo = applicationRepo;
        }


        // GET: Applications
        public async Task<IActionResult> Index()
        {

            List<Application> list = await _applicationRepo.GetApplicationsAsync();

            return View(list);
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int id)
        {
            
                ApplicationDetailsDTO application = await _applicationRepo.GetApplicationById(id);

                if (application == null)
                {
                    return NotFound();
                }

            

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewApplicationWthRdrctDTO model)
        {
            if (ModelState.IsValid)
            {

                Application app = _mapper.Map<NewApplicationDTO, Application>(model.application);

                await _applicationRepo.CreateApplicationAsync(app, model.redirectURI);

                await _applicationRepo.SaveChangesAsync();

                return RedirectToAction(actionName: "Index", controllerName: "Applications");
            }
            return View(model);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _dbContext.Applications == null)
            {
                return NotFound();
            }

            var application = await _dbContext.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationID,Name,Details,Url,Logo,UserId")] Application application)
        {
            if (id != application.ApplicationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(application);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbContext.Applications == null)
            {
                return NotFound();
            }

            var application = await _dbContext.Applications
                .FirstOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbContext.Applications == null)
            {
                return Problem("Entity set 'HexaDbdbContext.Applications'  is null.");
            }
            var application = await _dbContext.Applications.FindAsync(id);
            if (application != null)
            {
                _dbContext.Applications.Remove(application);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return (_dbContext.Applications?.Any(e => e.ApplicationID == id)).GetValueOrDefault();
        }
    }
}
