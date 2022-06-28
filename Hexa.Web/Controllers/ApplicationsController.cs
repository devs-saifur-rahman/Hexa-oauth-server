using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hexa.Data.DB;
using Hexa.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Hexa.Data.Models.oauth;

namespace Hexa.Web.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public ApplicationsController(AppDbContext dbContext, IHttpContextAccessor httpContext)
        { 
            _dbContext = dbContext;
            _httpContext = httpContext;
        }


        // GET: Applications
        public async Task<IActionResult> Index()
        {
              return _dbContext.Applications != null ? 
                          View(await _dbContext.Applications.ToListAsync()) :
                          Problem("Entity set 'HexaDbdbContext.Applications'  is null.");
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _dbContext.Applications == null)
            {
                return NotFound();
            }

            var application = await _dbContext.Applications
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
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

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Application model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(new Application
                {
                    ApplicationId = model.ApplicationId,
                    Name = model.Name,
                    Details = model.Details,
                    UserId = _httpContext.HttpContext.Session.Get<int>("User Id"),
                    Url = model.Url,
                    Logo = model.Logo
                });
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(actionName: "Index", controllerName:"Applications");
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
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,Name,Details,Url,Logo,UserId")] Application application)
        {
            if (id != application.ApplicationId)
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
                    if (!ApplicationExists(application.ApplicationId))
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
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
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
          return (_dbContext.Applications?.Any(e => e.ApplicationId == id)).GetValueOrDefault();
        }
    }
}
