using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld.Controllers
{
    public class EmpleosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empleos
        public async Task<IActionResult> Index(string busqueda, string ordenamiento)
        {
            ViewData["OrdenamientoTitulo"] = String.IsNullOrEmpty(ordenamiento) ? "titulo_desc" : "";
            ViewData["OrdenamientoFecha"] = ordenamiento == "Fecha" ? "fecha_desc" : "Fecha";
            ViewData["OrdenamientoSalario"] = ordenamiento == "Salario" ? "salario_desc" : "Salario";
            ViewData["FiltroActual"] = busqueda;

            var empleos = from m in _context.Empleos
                          select m;

            if (!String.IsNullOrEmpty(busqueda))
            {
                empleos = empleos.Where(s => s.Titulo.Contains(busqueda)
                                       || s.Categoria.Contains(busqueda));
            }

            switch (ordenamiento)
            {
                case "titulo_desc":
                    empleos = empleos.OrderByDescending(s => s.Titulo);
                    break;
                case "Fecha":
                    empleos = empleos.OrderBy(s => s.Fecha);
                    break;
                case "fecha_desc":
                    empleos = empleos.OrderByDescending(s => s.Fecha);
                    break;
                case "Salario":
                    empleos = empleos.OrderBy(s => s.Salario);
                    break;
                case "salario_desc":
                    empleos = empleos.OrderByDescending(s => s.Salario);
                    break;
                default:
                    empleos = empleos.OrderBy(s => s.Titulo);
                    break;
            }

            return View(await empleos.AsNoTracking().ToListAsync());
        }

        // GET: Empleos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleo = await _context.Empleos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleo == null)
            {
                return NotFound();
            }

            return View(empleo);
        }

        // GET: Empleos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Logo,Titulo,Categoria,Salario,Fecha,Enlace")] Empleo empleo)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(empleo.Logo))
                {
                    empleo.Logo = "/images/logo_por_defecto.png"; // Asegúrate de que esta ruta sea correcta.
                }

                _context.Add(empleo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleo);
        }

        // GET: Empleos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleo = await _context.Empleos.FindAsync(id);
            if (empleo == null)
            {
                return NotFound();
            }
            return View(empleo);
        }

        // POST: Empleos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Titulo,Categoria,Salario,Fecha,Enlace")] Empleo empleo)
        {
            if (id != empleo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(empleo.Logo))
                    {
                        empleo.Logo = "/images/logo_por_defecto.png"; // Asegúrate de que esta ruta sea correcta.
                    }

                    _context.Update(empleo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleoExists(empleo.Id))
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
            return View(empleo);
        }

        // GET: Empleos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleo = await _context.Empleos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleo == null)
            {
                return NotFound();
            }

            return View(empleo);
        }

        // POST: Empleos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleo = await _context.Empleos.FindAsync(id);
            _context.Empleos.Remove(empleo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleoExists(int id)
        {
            return _context.Empleos.Any(e => e.Id == id);
        }
    }
}
