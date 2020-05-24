
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mokki.Data;
using Mokki.Models;

namespace Mokki.Controllers
{
    public class EstanciasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EstanciasController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Estancias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Estancia.Include(e => e.Anfitrion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Estancias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estancia = await _context.Estancia
                .Include(e => e.Anfitrion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estancia == null)
            {
                return NotFound();
            }

            return View(estancia);
        }

        // GET: Estancias/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Estancias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Duracion,Foto")] Estancia estancia)
        {

            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Anfitrion anfitrion = await _context.Anfitriones.FirstOrDefaultAsync(x => x.UserId == user.Id);
            estancia.Anfitrion = anfitrion;
            estancia.AnfitrionId = anfitrion.Id;
            if (ModelState.IsValid)
            {
                _context.Add(estancia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(estancia);
        }

        // GET: Estancias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estancia = await _context.Estancia.FindAsync(id);
            if (estancia == null)
            {
                return NotFound();
            }
            ViewData["AnfitrionId"] = new SelectList(_context.Anfitriones, "Id", "Pueblo", estancia.AnfitrionId);
            return View(estancia);
        }

        // POST: Estancias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Duracion,Foto,AnfitrionId")] Estancia estancia)
        {
            if (id != estancia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estancia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstanciaExists(estancia.Id))
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
            ViewData["AnfitrionId"] = new SelectList(_context.Anfitriones, "Id", "Pueblo", estancia.AnfitrionId);
            return View(estancia);
        }

        // GET: Estancias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estancia = await _context.Estancia
                .Include(e => e.Anfitrion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estancia == null)
            {
                return NotFound();
            }

            return View(estancia);
        }

        // POST: Estancias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estancia = await _context.Estancia.FindAsync(id);
            _context.Estancia.Remove(estancia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstanciaExists(int id)
        {
            return _context.Estancia.Any(e => e.Id == id);
        }
    }
}
