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
    public class SubjectTeacherController : Controller
    {
        private readonly MBHS_Context _context;

        public SubjectTeacherController(MBHS_Context context)
        {
            _context = context;
        }

        // GET: SubjectTeacher
        public async Task<IActionResult> Index()
        {
            var mBHS_Context = _context.SubjectTeacher.Include(s => s.Subject).Include(s => s.Teacher);
            return View(await mBHS_Context.ToListAsync());
        }

        // GET: SubjectTeacher/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubjectTeacher == null)
            {
                return NotFound();
            }

            var subjectTeacher = await _context.SubjectTeacher
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.SubjectTeacherId == id);
            if (subjectTeacher == null)
            {
                return NotFound();
            }

            return View(subjectTeacher);
        }

        // GET: SubjectTeacher/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "SubjectId");
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: SubjectTeacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectTeacherId,SubjectId,TeacherId,Room")] SubjectTeacher subjectTeacher)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(subjectTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "SubjectId", subjectTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "UserName", subjectTeacher.TeacherId);
            return View(subjectTeacher);
        }

        // GET: SubjectTeacher/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubjectTeacher == null)
            {
                return NotFound();
            }

            var subjectTeacher = await _context.SubjectTeacher.FindAsync(id);
            if (subjectTeacher == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "SubjectId", subjectTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "UserName", subjectTeacher.TeacherId);
            return View(subjectTeacher);
        }

        // POST: SubjectTeacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectTeacherId,SubjectId,TeacherId,Room")] SubjectTeacher subjectTeacher)
        {
            if (id != subjectTeacher.SubjectTeacherId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectTeacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectTeacherExists(subjectTeacher.SubjectTeacherId))
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
            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "SubjectId", subjectTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "UserName", subjectTeacher.TeacherId);
            return View(subjectTeacher);
        }

        // GET: SubjectTeacher/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubjectTeacher == null)
            {
                return NotFound();
            }

            var subjectTeacher = await _context.SubjectTeacher
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.SubjectTeacherId == id);
            if (subjectTeacher == null)
            {
                return NotFound();
            }

            return View(subjectTeacher);
        }

        // POST: SubjectTeacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubjectTeacher == null)
            {
                return Problem("Entity set 'MBHS_Context.SubjectTeacher'  is null.");
            }
            var subjectTeacher = await _context.SubjectTeacher.FindAsync(id);
            if (subjectTeacher != null)
            {
                _context.SubjectTeacher.Remove(subjectTeacher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectTeacherExists(int id)
        {
          return (_context.SubjectTeacher?.Any(e => e.SubjectTeacherId == id)).GetValueOrDefault();
        }
    }
}
