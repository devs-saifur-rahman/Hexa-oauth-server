using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hexa.Data.DB;
using Hexa.Data.Models.oauth;

namespace Hexa.Web.Controllers
{
    public class ScopesController : Controller
    {
        private readonly AppDbContext _context;

        public ScopesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Scopes
        public async Task<IActionResult> Index()
        {
              return _context.Scopes != null ? 
                          View(await _context.Scopes.ToListAsync()) :
                          Problem("Entity set 'HexaDbContext.Scopes'  is null.");
        }

        // GET: Scopes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Scopes == null)
            {
                return NotFound();
            }

            var scope = await _context.Scopes
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
                _context.Add(scope);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scope);
        }

        // GET: Scopes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Scopes == null)
            {
                return NotFound();
            }

            var scope = await _context.Scopes.FindAsync(id);
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
                    _context.Update(scope);
                    await _context.SaveChangesAsync();
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
            if (id == null || _context.Scopes == null)
            {
                return NotFound();
            }

            var scope = await _context.Scopes
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
            if (_context.Scopes == null)
            {
                return Problem("Entity set 'HexaDbContext.Scopes'  is null.");
            }
            var scope = await _context.Scopes.FindAsync(id);
            if (scope != null)
            {
                _context.Scopes.Remove(scope);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScopeExists(int id)
        {
          return (_context.Scopes?.Any(e => e.ScopeId == id)).GetValueOrDefault();
        }
    }
}
