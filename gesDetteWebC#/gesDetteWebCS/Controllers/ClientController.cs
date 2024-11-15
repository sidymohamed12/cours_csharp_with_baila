using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gesDetteWebCS.Models;
using gesDetteWebCS.Data;

namespace gesDetteWebCS.Controllers
{
    using BCrypt.Net;
    using gesDetteWebCS.Models.enums;
    using Microsoft.IdentityModel.Tokens;

    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3, string? search = null)
        {
            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(cl => cl.Surnom.Contains(search));
            }

            var clients = await query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();

            int totalClients = await query.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalClients / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.SearchQuery = search;

            return View(clients);
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
        .Include(c => c.Dettes)
        .FirstOrDefaultAsync(m => m.Id == id);

            if (client == null)
            {
                return NotFound();
            }
            ViewData["TotalMontantDettes"] = client.Dettes?.Sum(d => d.Montant) ?? 0;
            ViewData["TotalMontantVerserDettes"] = client.Dettes?.Sum(d => d.MontantVerser) ?? 0;
            ViewData["TotalMontantRestantDettes"] = (double)ViewData["TotalMontantDettes"] - (double)ViewData["TotalMontantVerserDettes"];
            return View(client);
        }


        // GET: Client/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Surnom,Telephone,Adresse,Id,CreatedAt,UpdatedAt,User")] Client client)
        {
            if (Request.Form["toggleSwitch"] != "on")
            {
                client.User = null;
                ModelState.Remove("User.Login");
                ModelState.Remove("User.Password");
            }

            if (ModelState.IsValid)
            {
                client.onPrePersist();

                // Si le toggleSwitch est activé, configure l'utilisateur
                if (Request.Form["toggleSwitch"] == "on" && client.User != null)
                {
                    client.User.Role = Role.client;
                    client.User.Etat = true;
                    client.User.onPrePersist();
                    client.User.Password = BCrypt.HashPassword(client.User.Password);
                    _context.Add(client.User);
                }

                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Client/Edit/5
        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Surnom,Telephone,Adresse,Id,CreatedAt,UpdatedAt")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
