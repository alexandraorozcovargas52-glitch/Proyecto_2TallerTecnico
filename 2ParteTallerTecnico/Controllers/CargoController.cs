using _2tallertecnico.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _2tallertecnico.Controllers
{
    public class CargoController : Controller
    {
        private readonly EscuelaContext _context;

        public CargoController(EscuelaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cargos = _context.Cargos.ToList();
            return View(cargos);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                _context.Cargos.Add(cargo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var cargo = _context.Cargos.Find(id);
            if (cargo == null)
                return NotFound();

            return View(cargo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cargo cargo)
        {
            if (id != cargo.CargoId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(cargo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var cargo = _context.Cargos.Find(id);
            if (cargo == null)
                return NotFound();

            return View(cargo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cargo = _context.Cargos.Find(id);
            if (cargo != null)
            {
                _context.Cargos.Remove(cargo);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
