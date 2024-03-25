using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

namespace School.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AssignedMaterialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignedMaterialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssignedMaterials
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.AssignedMaterial.Include(a => a.Student).Include(a => a.Subject);
            return View(await schoolContext.ToListAsync());
        }

        // GET: AssignedMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedMaterial = await _context.AssignedMaterial
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(m => m.AssignedMaterialID == id);
            if (assignedMaterial == null)
            {
                return NotFound();
            }

            return View(assignedMaterial);
        }

        // GET: AssignedMaterials/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentName");
            ViewData["SubjectID"] = new SelectList(_context.Subject, "SubjectID", "SubjectName");
            return View();
        }

        // POST: AssignedMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignedMaterialID,StudentID,SubjectID")] AssignedMaterial assignedMaterial)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(assignedMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentName", assignedMaterial.StudentID);
            ViewData["SubjectID"] = new SelectList(_context.Subject, "SubjectID", "SubjectName", assignedMaterial.SubjectID);
            return View(assignedMaterial);
        }

        // GET: AssignedMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedMaterial = await _context.AssignedMaterial.FindAsync(id);
            if (assignedMaterial == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentName", assignedMaterial.StudentID);
            ViewData["SubjectID"] = new SelectList(_context.Subject, "SubjectID", "SubjectName", assignedMaterial.SubjectID);
            return View(assignedMaterial);
        }

        // POST: AssignedMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignedMaterialID,StudentID,SubjectID")] AssignedMaterial assignedMaterial)
        {
            if (id != assignedMaterial.AssignedMaterialID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignedMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignedMaterialExists(assignedMaterial.AssignedMaterialID))
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
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentName", assignedMaterial.StudentID);
            ViewData["SubjectID"] = new SelectList(_context.Subject, "SubjectID", "SubjectName", assignedMaterial.SubjectID);
            return View(assignedMaterial);
        }

        // GET: AssignedMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedMaterial = await _context.AssignedMaterial
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(m => m.AssignedMaterialID == id);
            if (assignedMaterial == null)
            {
                return NotFound();
            }

            return View(assignedMaterial);
        }

        // POST: AssignedMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignedMaterial = await _context.AssignedMaterial.FindAsync(id);
            if (assignedMaterial != null)
            {
                _context.AssignedMaterial.Remove(assignedMaterial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignedMaterialExists(int id)
        {
            return _context.AssignedMaterial.Any(e => e.AssignedMaterialID == id);
        }
    }
}
