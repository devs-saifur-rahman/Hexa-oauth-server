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
    public class GrantTypeController : Controller
    {
        private readonly HexaDbContext _context;

        public GrantTypeController(HexaDbContext context)
        {
            _context = context;
        }

        // GET: GrantTypes
        public async Task<IActionResult> Index()
        {
              return _context.GrantTypes != null ? 
                          View(await _context.GrantTypes.ToListAsync()) :
                          Problem("Entity set 'HexaDbContext.GrantTypes'  is null.");
        }

        // GET: GrantTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GrantTypes == null)
            {
                return NotFound();
            }

            var grantType = await _context.GrantTypes
                .FirstOrDefaultAsync(m => m.id == id);
            if (grantType == null)
            {
                return NotFound();
            }

            return View(grantType);
        }

        // GET: GrantTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrantTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Description")] GrantType grantType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grantType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grantType);
        }

        // GET: GrantTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GrantTypes == null)
            {
                return NotFound();
            }

            var grantType = await _context.GrantTypes.FindAsync(id);
            if (grantType == null)
            {
                return NotFound();
            }
            return View(grantType);
        }

        // POST: GrantTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Description")] GrantType grantType)
        {
            if (id != grantType.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grantType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrantTypeExists(grantType.id))
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
            return View(grantType);
        }

        // GET: GrantTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GrantTypes == null)
            {
                return NotFound();
            }

            var grantType = await _context.GrantTypes
                .FirstOrDefaultAsync(m => m.id == id);
            if (grantType == null)
            {
                return NotFound();
            }

            return View(grantType);
        }

        // POST: GrantTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GrantTypes == null)
            {
                return Problem("Entity set 'HexaDbContext.GrantTypes'  is null.");
            }
            var grantType = await _context.GrantTypes.FindAsync(id);
            if (grantType != null)
            {
                _context.GrantTypes.Remove(grantType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrantTypeExists(int id)
        {
          return (_context.GrantTypes?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
