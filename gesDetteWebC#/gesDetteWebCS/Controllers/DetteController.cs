using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gesDetteWebCS.Data;
using gesDetteWebCS.Models;

namespace gesDetteWebCS.Controllers
{
    public class DetteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dette
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dettes.ToListAsync());
        }

        // GET: Dette/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Dette/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dette/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Montant,MontantVerser,MontantRestant,Archiver,Date,EtatD,Id,CreatedAt,UpdatedAt")] Dette dette)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
