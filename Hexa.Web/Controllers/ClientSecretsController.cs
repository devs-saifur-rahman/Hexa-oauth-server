using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hexa.Data.DB;
using Hexa.Data.Models.oauth;

namespace Hexa.Web.Controllers
{
    public class ClientSecretsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientSecretsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ClientSecrets
        public async Task<IActionResult> Index()
        {
            var hexaDbContext = _context.ClientSecrets.Include(c => c.Application);
            return View(await hexaDbContext.ToListAsync());
        }

        // GET: ClientSecrets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientSecrets == null)
            {
                return NotFound();
            }

            var clientSecret = await _context.ClientSecrets
                .Include(c => c.Application)
                .FirstOrDefaultAsync(m => m.ClientSecretID == id);
            if (clientSecret == null)
            {
                return NotFound();
            }

            return View(clientSecret);
        }

        // GET: ClientSecrets/Create
        public IActionResult Create()
        {
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationID");
            return View();
        }

        // POST: ClientSecrets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientSecretID,ClientID,Secret,IsActive,ApplicationID")] ClientSecret clientSecret)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientSecret);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationID", clientSecret.ApplicationID);
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
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationID", clientSecret.ApplicationID);
            return View(clientSecret);
        }

        // POST: ClientSecrets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientSecretID,ClientID,Secret,IsActive,ApplicationID")] ClientSecret clientSecret)
        {
            if (id != clientSecret.ClientSecretID)
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
                    if (!ClientSecretExists(clientSecret.ClientSecretID))
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
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationID", clientSecret.ApplicationID);
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
                .Include(c => c.Application)
                .FirstOrDefaultAsync(m => m.ClientSecretID == id);
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
          return (_context.ClientSecrets?.Any(e => e.ClientSecretID == id)).GetValueOrDefault();
        }
    }
}
