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
    public class PayementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PayementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payement
        public async Task<IActionResult> Index()
        {
            return View(await _context.Payements.ToListAsync());
        }

        // GET: Payement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payement = await _context.Payements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payement == null)
            {
                return NotFound();
            }

            return View(payement);
        }

        // GET: Payement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Montant,Id,CreatedAt,UpdatedAt")] Payement payement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payement);
        }

        // GET: Payement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payement = await _context.Payements.FindAsync(id);
            if (payement == null)
            {
                return NotFound();
            }
            return View(payement);
        }

        // POST: Payement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Date,Montant,Id,CreatedAt,UpdatedAt")] Payement payement)
        {
            if (id != payement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayementExists(payement.Id))
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
            return View(payement);
        }

        // GET: Payement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payement = await _context.Payements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payement == null)
            {
                return NotFound();
            }

            return View(payement);
        }

        // POST: Payement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payement = await _context.Payements.FindAsync(id);
            if (payement != null)
            {
                _context.Payements.Remove(payement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayementExists(int id)
        {
            return _context.Payements.Any(e => e.Id == id);
        }
    }
}
