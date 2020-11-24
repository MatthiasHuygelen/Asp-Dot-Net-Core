using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scaffolding.Data;
using Scaffolding.Domain;

namespace Scaffolding.Controllers
{
    public class BoekenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoekenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Boeken
        public async Task<IActionResult> Index()
        {
            return View(await _context.Boeken.ToListAsync());
        }

        // GET: Boeken/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boek = await _context.Boeken
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boek == null)
            {
                return NotFound();
            }

            return View(boek);
        }

        // GET: Boeken/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boeken/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titel,Jaar,AantalPaginas")] Boek boek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boek);
        }

        // GET: Boeken/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boek = await _context.Boeken.FindAsync(id);
            if (boek == null)
            {
                return NotFound();
            }
            return View(boek);
        }

        // POST: Boeken/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titel,Jaar,AantalPaginas")] Boek boek)
        {
            if (id != boek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoekExists(boek.Id))
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
            return View(boek);
        }

        // GET: Boeken/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boek = await _context.Boeken
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boek == null)
            {
                return NotFound();
            }

            return View(boek);
        }

        // POST: Boeken/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boek = await _context.Boeken.FindAsync(id);
            _context.Boeken.Remove(boek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoekExists(int id)
        {
            return _context.Boeken.Any(e => e.Id == id);
        }
    }
}
