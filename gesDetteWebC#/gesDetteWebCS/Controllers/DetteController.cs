using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gesDetteWebCS.Data;
using gesDetteWebCS.Models;
using gesDetteWebCS.Models.enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace gesDetteWebCS.Controllers
{
    public class DetteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? etat, int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.Dettes.Include(d => d.ClientD).AsQueryable();

            if (etat.HasValue)
            {
                query = etat.Value == 1
                    ? query.Where(d => d.Montant.Equals(d.MontantVerser)) // Soldé
                    : query.Where(d => !d.Montant.Equals(d.MontantVerser)); // Non soldé
            }
            ViewBag.SelectedEtat = etat;

            int totalDettes = await query.CountAsync();

            // Pagination sur les dettes filtrées
            var dettes = await query.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling(totalDettes / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(dettes);
        }

        // GET: Dette/Details/5
        public async Task<IActionResult> Details(int? id, string? prixPayer)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dette = await _context.Dettes
                .Include(d => d.ClientD)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dette == null)
            {
                return NotFound();
            }


            if (!string.IsNullOrEmpty(prixPayer))
            {
                if (Convert.ToDouble(prixPayer) > dette.MontantRestant || Convert.ToDouble(prixPayer) <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Le montant à payer dépasse le montant restant à payer ou invalide.");
                    return View(dette);
                }
                Payement payement = new()
                {
                    Date = DateTime.UtcNow,
                    Dette = dette,
                    Montant = Convert.ToDouble(prixPayer),
                };
                payement.onPrePersist();
                _context.Add(payement);
                dette.MontantVerser += Convert.ToDouble(prixPayer);
                dette.onPreUpdated();
                _context.Update(dette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(dette);
        }

        // GET: Dette/Create
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var clients = await _context.Clients.ToListAsync();
            ViewData["Clients"] = clients;

            var articles = await _context.Articles.ToListAsync();
            ViewData["Articles"] = articles;

            var panier = HttpContext.Session.GetObjectFromJson<Panier>("Panier") ?? new Panier();
            ViewData["Panier"] = panier;
            return View();
        }


        [HttpPost]
        public IActionResult AddArticle(int articleId, int qteVendu)
        {
            if (ModelState.IsValid)
            {
                var article = _context.Articles.Find(articleId);
                if (article == null)
                {
                    ModelState.AddModelError("", "Selection un article !");
                    return RedirectToAction("Create");
                }

                if (qteVendu == 0)
                {
                    ModelState.AddModelError("", "Sqte non valide !");
                    return RedirectToAction("Create");
                }
                var panier = HttpContext.Session.GetObjectFromJson<Panier>("Panier") ?? new Panier();
                panier.AddinPanier(article, qteVendu);

                HttpContext.Session.SetObjectAsJson("Panier", panier);

                return RedirectToAction("Create");
            }
            return RedirectToAction("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dette dette, int? clientId)
        {

            if (ModelState.IsValid)
            {
                var panier = HttpContext.Session.GetObjectFromJson<Panier>("Panier");
                if (panier == null || !panier.ArticlesPanier.Any())
                {
                    ModelState.AddModelError("", "Le panier est vide.");
                    return RedirectToAction("Create");
                }

                var client = await _context.Clients.FindAsync(clientId);
                if (client == null)
                {
                    ModelState.AddModelError("ClientD", "Client introuvable.");
                    return RedirectToAction("Create");
                }

                dette.ClientD = client;
                dette.Montant = panier.Total;
                dette.MontantVerser = 0.0;
                dette.EtatD = Etat.encours;
                dette.Archiver = false;
                dette.Date = DateTime.UtcNow;
                dette.onPrePersist();
                dette.Details = panier.ArticlesPanier;
                foreach (var item in panier.ArticlesPanier)
                {
                    _context.Attach(item.Article);
                    item.Dette = dette;
                    item.onPrePersist();
                    _context.Add(item);
                }
                _context.Add(dette);
                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("Panier");
                return RedirectToAction(nameof(Index));
            }
            ViewData["Clients"] = await _context.Clients.ToListAsync();
            ViewData["Articles"] = await _context.Clients.ToListAsync();
            return View(dette);
        }

        // GET: Dette/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dette = await _context.Dettes.FindAsync(id);
            if (dette == null)
            {
                return NotFound();
            }
            return View(dette);
        }

        // POST: Dette/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Montant,MontantVerser,MontantRestant,Archiver,Date,EtatD,Id,CreatedAt,UpdatedAt")] Dette dette)
        {
            if (id != dette.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetteExists(dette.Id))
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
            return View(dette);
        }

        // GET: Dette/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dette = await _context.Dettes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dette == null)
            {
                return NotFound();
            }

            return View(dette);
        }

        // POST: Dette/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dette = await _context.Dettes.FindAsync(id);
            if (dette != null)
            {
                _context.Dettes.Remove(dette);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteArticle(int articleId)
        {
            var panier = HttpContext.Session.GetObjectFromJson<Panier>("Panier");
            if (panier != null)
            {
                panier = panier.DeleteArticle(articleId);
                HttpContext.Session.SetObjectAsJson("Panier", panier);
                ViewData["Panier"] = panier;
            }
            return RedirectToAction("Create");
        }

        private bool DetteExists(int id)
        {
            return _context.Dettes.Any(e => e.Id == id);
        }
    }
}
