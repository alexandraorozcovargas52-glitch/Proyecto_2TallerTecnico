using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2tallertecnico.Models;

namespace _2tallertecnico.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly EscuelaContext _context;

        public UsuarioController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            try
            {
                var usuarios = await _context.Usuarios
                    .Include(u => u.Cargo)
                    .OrderBy(u => u.IdUsuario)
                    .ToListAsync();

                return View(usuarios);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new System.Collections.Generic.List<Usuario>());
            }
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Cargo)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            ViewData["IdCargo"] = new SelectList(_context.Cargos, "IdCargo", "NombreCargo");
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioNombre,Apellido,Email,Password,Estado,IdCargo")] Usuario usuario)
        {
            try
            {
                // Quitar validaciones del ModelState para propiedades que no necesitamos
                ModelState.Remove("Cargo");
                ModelState.Remove("NombreCargo");

                if (ModelState.IsValid)
                {
                    usuario.Fecha_Registro = DateTime.Now;
                    usuario.Estado = true;

                    // Si no se proporciona contraseña, poner una por defecto
                    if (string.IsNullOrEmpty(usuario.Password))
                    {
                        usuario.Password = "123456"; // Contraseña por defecto
                    }

                    _context.Add(usuario);
                    await _context.SaveChangesAsync();

                    TempData["Mensaje"] = "Usuario creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el usuario: " + ex.Message);
            }

            ViewData["IdCargo"] = new SelectList(_context.Cargos, "IdCargo", "NombreCargo", usuario.IdCargo);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            ViewData["IdCargo"] = new SelectList(_context.Cargos, "IdCargo", "NombreCargo", usuario.IdCargo);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,UsuarioNombre,Apellido,Email,Password,Estado,Fecha_Registro,IdCargo")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            try
            {
                // Quitar validaciones del ModelState
                ModelState.Remove("Cargo");
                ModelState.Remove("NombreCargo");

                if (ModelState.IsValid)
                {
                    // Obtener el usuario actual de la BD
                    var usuarioActual = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.IdUsuario == id);

                    if (usuarioActual == null)
                    {
                        return NotFound();
                    }

                    // Si no se cambió la contraseña, mantener la anterior
                    if (string.IsNullOrWhiteSpace(usuario.Password))
                    {
                        usuario.Password = usuarioActual.Password;
                    }

                    // Mantener la fecha de registro original
                    usuario.Fecha_Registro = usuarioActual.Fecha_Registro;

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();

                    TempData["Mensaje"] = "Usuario actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.IdUsuario))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar: " + ex.Message);
            }

            ViewData["IdCargo"] = new SelectList(_context.Cargos, "IdCargo", "NombreCargo", usuario.IdCargo);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Cargo)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Usuario eliminado exitosamente";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}