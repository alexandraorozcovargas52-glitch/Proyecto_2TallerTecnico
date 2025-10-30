using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2tallertecnico.Models;

namespace _2tallertecnico.Controllers
{
    public class PersonaController : Controller
    {
        private readonly EscuelaContext _context;

        public PersonaController(EscuelaContext context)
        {
            _context = context;
        }

        // ✅ LISTAR
        public async Task<IActionResult> Index()
        {
            var personas = await _context.Personas.ToListAsync();
            return View(personas);
        }

        // ✅ DETALLES
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.IdPersona == id);
            if (persona == null) return NotFound();

            return View(persona);
        }

        // ✅ CREAR
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPersona,Nombre,Apellido,Telefono,Direccion,FechaNacimiento,Estado")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Persona creada exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // ✅ EDITAR
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();

            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPersona,Nombre,Apellido,Telefono,Direccion,FechaNacimiento,Estado")] Persona persona)
        {
            if (id != persona.IdPersona) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Persona actualizada exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Personas.Any(e => e.IdPersona == persona.IdPersona))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // ✅ ELIMINAR
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.IdPersona == id);
            if (persona == null) return NotFound();

            return View(persona);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Persona eliminada exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
