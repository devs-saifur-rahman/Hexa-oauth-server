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
    public class ClientSecretController : Controller
    {
        private readonly HexaDbContext _context;

        public ClientSecretController(HexaDbContext context)
        {
            _context = context;
        }

        // GET: ClientSecrets
        public async Task<IActionResult> Index()
        {
              return _context.ClientSecrets != null ? 
                          View(await _context.ClientSecrets.ToListAsync()) :
                          Problem("Entity set 'HexaDbContext.ClientSecrets'  is null.");
        }

        // GET: ClientSecrets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientSecrets == null)
            {
                return NotFound();
            }

            var clientSecret = await _context.ClientSecrets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clientSecret == null)
            {
                return NotFound();
            }

            return View(clientSecret);
        }

        // GET: ClientSecrets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientSecrets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AppID,ClientID,Secret,IsActive")] ClientSecret clientSecret)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientSecret);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientSecret);
        }

        // GET: ClientSecrets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientSecrets == null)
            {
                return NotFound();
            }

            var clientSecret = await _context.ClientSecrets.FindAsync(id);
            if (clientSecret == null)
            {
                return NotFound();
            }
            return View(clientSecret);
        }

        // POST: ClientSecrets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AppID,ClientID,Secret,IsActive")] ClientSecret clientSecret)
        {
            if (id != clientSecret.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientSecret);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientSecretExists(clientSecret.ID))
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
            return View(clientSecret);
        }

        // GET: ClientSecrets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientSecrets == null)
            {
                return NotFound();
            }

            var clientSecret = await _context.ClientSecrets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clientSecret == null)
            {
                return NotFound();
            }

            return View(clientSecret);
        }

        // POST: ClientSecrets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClientSecrets == null)
            {
                return Problem("Entity set 'HexaDbContext.ClientSecrets'  is null.");
            }
            var clientSecret = await _context.ClientSecrets.FindAsync(id);
            if (clientSecret != null)
            {
                _context.ClientSecrets.Remove(clientSecret);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientSecretExists(int id)
        {
          return (_context.ClientSecrets?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
