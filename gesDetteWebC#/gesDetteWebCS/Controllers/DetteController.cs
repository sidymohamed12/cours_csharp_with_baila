using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gesDetteWebCS.Data;
using gesDetteWebCS.Models;
using gesDetteWebCS.Models.enums;

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

            if (!string.IsNullOrEmpty(prixPayer) && double.TryParse(prixPayer, out double montant) && montant > 0)
            {
                Payement payement = new Payement
                {
                    Date = DateTime.UtcNow,
                    Dette = dette,
                    Montant = Convert.ToDouble(prixPayer),
                };
                payement.onPrePersist();
                _context.Add(payement);
                dette.MontantVerser += Convert.ToDouble(prixPayer);
                dette.onPreUpdated();
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

            ViewData["Panier"] = TempData["Panier"] as List<Detail> ?? new List<Detail>();
            return View();
        }


        [HttpPost]
        public IActionResult AddArticle(int articleId, int qteVendu)
        {
            var article = _context.Articles.Find(articleId);
            if (article != null)
            {
                var detail = new Detail
                {
                    Article = article,
                    QteVendu = qteVendu,
                    MontantVendu = article.Prix * qteVendu
                };

                // Récupérer le panier depuis TempData
                var panier = TempData["Panier"] as List<Detail> ?? new List<Detail>();
                panier.Add(detail);

                // Stocker le panier mis à jour dans TempData
                TempData["Panier"] = panier;
                TempData.Keep("Panier");
            }
            return RedirectToAction("Create");
        }
        // POST: Dette/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> CreateWithArticle([Bind("Montant,MontantVerser,Archiver,Date,EtatD,ClientD")] Dette dette)
        // {
        //     // Récupérer le panier des articles depuis TempData
        //     var panier = TempData["Panier"] as List<Detail>;

        //     if (panier != null && ModelState.IsValid)
        //     {
        //         dette.Details = panier;
        //         dette.Montant = panier.Sum(d => d.MontantVendu);
        //         dette.MontantVerser = 0.0;
        //         dette.Date = DateTime.UtcNow;
        //         dette.EtatD = Models.enums.Etat.encours;
        //         dette.Archiver = false;
        //         dette.onPrePersist();
        //         _context.Add(dette);
        //         await _context.SaveChangesAsync();

        //         // Réinitialiser le panier après sauvegarde
        //         TempData.Remove("Panier");

        //         return RedirectToAction(nameof(Index));
        //     }

        //     ViewData["Clients"] = await _context.Clients.ToListAsync();
        //     ViewData["Articles"] = await _context.Articles.ToListAsync();
        //     ViewData["Panier"] = panier;
        //     return View(dette);
        // }



        // ----------créer sans article --------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dette dette, int? clientId)
        {

            if (ModelState.IsValid)
            {
                var client = await _context.Clients.FindAsync(clientId);

                if (client == null)
                {
                    ModelState.AddModelError("ClientD", "Client introuvable.");
                    return View(dette);
                }
                dette.ClientD = client;
                dette.MontantVerser = 0.0;
                dette.EtatD = Etat.encours;
                dette.Archiver = false;
                dette.Date = DateTime.UtcNow;
                dette.onPrePersist();
                _context.Add(dette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Clients"] = await _context.Clients.ToListAsync();
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

        private bool DetteExists(int id)
        {
            return _context.Dettes.Any(e => e.Id == id);
        }
    }
}
