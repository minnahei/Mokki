using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mokki.Data;
using Mokki.Models;

namespace Mokki.Controllers
{
    public class AnfitrionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnfitrionesController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: Anfitriones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Anfitriones.Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Anfitriones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anfitrion = await _context.Anfitriones
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anfitrion == null)
            {
                return NotFound();
            }

            return View(anfitrion);
        }

        // GET: Anfitriones/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Anfitriones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pueblo,UserId")] Anfitrion anfitrion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anfitrion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", anfitrion.UserId);
            return View(anfitrion);
        }

        // GET: Anfitriones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anfitrion = await _context.Anfitriones.FindAsync(id);
            if (anfitrion == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", anfitrion.UserId);
            return View(anfitrion);
        }

        // POST: Anfitriones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pueblo,UserId")] Anfitrion anfitrion)
        {
            if (id != anfitrion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anfitrion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnfitrionExists(anfitrion.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", anfitrion.UserId);
            return View(anfitrion);
        }

        // GET: Anfitriones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anfitrion = await _context.Anfitriones
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anfitrion == null)
            {
                return NotFound();
            }

            return View(anfitrion);
        }

        // POST: Anfitriones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anfitrion = await _context.Anfitriones.FindAsync(id);
            _context.Anfitriones.Remove(anfitrion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnfitrionExists(int id)
        {
            return _context.Anfitriones.Any(e => e.Id == id);
        }
    }
}
