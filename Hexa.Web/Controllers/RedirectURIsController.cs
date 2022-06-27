using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hexa.Web.DB;
using Hexa.Web.Models.oatuh;

namespace Hexa.Web.Controllers
{
    public class RedirectURIsController : Controller
    {
        private readonly HexaDbContext _context;

        public RedirectURIsController(HexaDbContext context)
        {
            _context = context;
        }

        // GET: RedirectURIs
        public async Task<IActionResult> Index()
        {
            var hexaDbContext = _context.RedirectUris.Include(r => r.Application);
            return View(await hexaDbContext.ToListAsync());
        }

        // GET: RedirectURIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RedirectUris == null)
            {
                return NotFound();
            }

            var redirectURI = await _context.RedirectUris
                .Include(r => r.Application)
                .FirstOrDefaultAsync(m => m.RedirectURIId == id);
            if (redirectURI == null)
            {
                return NotFound();
            }

            return View(redirectURI);
        }

        // GET: RedirectURIs/Create
        public IActionResult Create()
        {
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "ApplicationId");
            return View();
        }

        // POST: RedirectURIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RedirectURIId,URI,IsActive,ApplicationId")] RedirectURI redirectURI)
        {
            if (ModelState.IsValid)
            {
                _context.Add(redirectURI);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "ApplicationId", redirectURI.ApplicationId);
            return View(redirectURI);
        }

        // GET: RedirectURIs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RedirectUris == null)
            {
                return NotFound();
            }

            var redirectURI = await _context.RedirectUris.FindAsync(id);
            if (redirectURI == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "ApplicationId", redirectURI.ApplicationId);
            return View(redirectURI);
        }

        // POST: RedirectURIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RedirectURIId,URI,IsActive,ApplicationId")] RedirectURI redirectURI)
        {
            if (id != redirectURI.RedirectURIId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(redirectURI);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RedirectURIExists(redirectURI.RedirectURIId))
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
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "ApplicationId", redirectURI.ApplicationId);
            return View(redirectURI);
        }

        // GET: RedirectURIs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RedirectUris == null)
            {
                return NotFound();
            }

            var redirectURI = await _context.RedirectUris
                .Include(r => r.Application)
                .FirstOrDefaultAsync(m => m.RedirectURIId == id);
            if (redirectURI == null)
            {
                return NotFound();
            }

            return View(redirectURI);
        }

        // POST: RedirectURIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RedirectUris == null)
            {
                return Problem("Entity set 'HexaDbContext.RedirectUris'  is null.");
            }
            var redirectURI = await _context.RedirectUris.FindAsync(id);
            if (redirectURI != null)
            {
                _context.RedirectUris.Remove(redirectURI);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RedirectURIExists(int id)
        {
          return (_context.RedirectUris?.Any(e => e.RedirectURIId == id)).GetValueOrDefault();
        }
    }
}
