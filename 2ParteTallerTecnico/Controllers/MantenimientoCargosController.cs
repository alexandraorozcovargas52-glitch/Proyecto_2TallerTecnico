
using _2tallertecnico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2tallertecnico.Controllers
{
    public class MantenimientoCargosController : Controller
    {
        private readonly EscuelaContext _context;

        public MantenimientoCargosController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: MantenimientoCargos
        public async Task<IActionResult> Index()
        {
            var cargos = await _context.Cargos
                .Include(c => c.Usuarios)
                .ToListAsync();
            return View(cargos);
        }

        // GET: MantenimientoCargos/Activar/5
        public async Task<IActionResult> Activar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            cargo.Estado = true;
            _context.Update(cargo);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"Cargo '{cargo.NombreCargo}' activado exitosamente";

            return RedirectToAction(nameof(Index));
        }

        // GET: MantenimientoCargos/Desactivar/5
        public async Task<IActionResult> Desactivar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            cargo.Estado = false;
            _context.Update(cargo);
            await _context.SaveChangesAsync();
            TempData["Warning"] = $"Cargo '{cargo.NombreCargo}' desactivado";

            return RedirectToAction(nameof(Index));
        }
    }
}