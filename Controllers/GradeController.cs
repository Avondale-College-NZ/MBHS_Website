using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBHS_Website.Areas.Identity.Data;
using MBHS_Website.Models;

namespace MBHS_Website.Controllers
{
    public class GradeController : Controller
    {
        private readonly MBHS_Context _context;

        public GradeController(MBHS_Context context)
        {
            _context = context;
        }

        // GET: Grade
        public async Task<IActionResult> Index()
        {
            var mBHS_Context = _context.Grade.Include(g => g.Exam).Include(g => g.Student);
            return View(await mBHS_Context.ToListAsync());
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .Include(g => g.Exam)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            ViewData["ExamId"] = new SelectList(_context.Set<Exam>(), "ExamId", "ExamId");
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "StudentId", "StudentId");
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeId,Mark,ExamId,StudentId")] Grade grade)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExamId"] = new SelectList(_context.Set<Exam>(), "ExamId", "ExamId", grade.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "StudentId", "StudentId", grade.StudentId);
            return View(grade);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewData["ExamId"] = new SelectList(_context.Set<Exam>(), "ExamId", "ExamId", grade.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "StudentId", "StudentId", grade.StudentId);
            return View(grade);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeId,Mark,ExamId,StudentId")] Grade grade)
        {
            if (id != grade.GradeId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeId))
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
            ViewData["ExamId"] = new SelectList(_context.Set<Exam>(), "ExamId", "ExamId", grade.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "StudentId", "StudentId", grade.StudentId);
            return View(grade);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .Include(g => g.Exam)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grade == null)
            {
                return Problem("Entity set 'MBHS_Context.Grade'  is null.");
            }
            var grade = await _context.Grade.FindAsync(id);
            if (grade != null)
            {
                _context.Grade.Remove(grade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
          return (_context.Grade?.Any(e => e.GradeId == id)).GetValueOrDefault();
        }
    }
}
