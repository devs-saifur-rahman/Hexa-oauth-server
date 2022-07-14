using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hexa.Data.DB;
using Hexa.Data.Models.oauth;
using Hexa.Data.Repositories;
using AutoMapper;
using Hexa.Data.DTOs;

namespace Hexa.Web.Controllers
{
    public class ScopesController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IScopeRepo _scopeRepo;
        private readonly IMapper _mapper;

        public ScopesController(AppDbContext dbContext,IHttpContextAccessor httpContextAccessor, IMapper mapper, IScopeRepo scopeRepo)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _scopeRepo = scopeRepo;
        }

        // GET: Scopes
        public async Task<IActionResult> Index()
        {
            //return _dbContext.Scopes != null ? 
            //            View(await _dbContext.Scopes.ToListAsync()) :
            //            Problem("Entity set 'HexaDbContext.Scopes'  is null.");

            
            
           List<ScopeDTO> listScope = _mapper.Map<List<Scope>, List<ScopeDTO>>(await _scopeRepo.GetScopesAsync());

            return View(listScope);

        }

        // GET: Scopes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _dbContext.Scopes == null)
            {
                return NotFound();
            }

            var scope = await _dbContext.Scopes
                .FirstOrDefaultAsync(m => m.ScopeId == id);
            if (scope == null)
            {
                return NotFound();
            }

            return View(scope);
        }

        // GET: Scopes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scopes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Tag")] Scope scope)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(scope);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scope);
        }

        // GET: Scopes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _dbContext.Scopes == null)
            {
                return NotFound();
            }

            var scope = await _dbContext.Scopes.FindAsync(id);
            if (scope == null)
            {
                return NotFound();
            }
            return View(scope);
        }

        // POST: Scopes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Tag")] Scope scope)
        {
            if (id != scope.ScopeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(scope);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScopeExists(scope.ScopeId))
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
            return View(scope);
        }

        // GET: Scopes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbContext.Scopes == null)
            {
                return NotFound();
            }

            var scope = await _dbContext.Scopes
                .FirstOrDefaultAsync(m => m.ScopeId == id);
            if (scope == null)
            {
                return NotFound();
            }

            return View(scope);
        }

        // POST: Scopes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbContext.Scopes == null)
            {
                return Problem("Entity set 'HexaDbContext.Scopes'  is null.");
            }
            var scope = await _dbContext.Scopes.FindAsync(id);
            if (scope != null)
            {
                _dbContext.Scopes.Remove(scope);
            }
            
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScopeExists(int id)
        {
          return (_dbContext.Scopes?.Any(e => e.ScopeId == id)).GetValueOrDefault();
        }
    }
}
