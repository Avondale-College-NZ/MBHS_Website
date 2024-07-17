using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBHS_Website.Areas.Identity.Data;
using MBHS_Website.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;

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
            var mBHS_Context = _context.Grade.Include(g => g.Exam).Include(g => g.Student).Include(g => g.Exam.Subject);
            return View(await mBHS_Context.ToListAsync());
        }

        // GET: Grade/Details/5
        [Authorize(Roles = "Admin,Manager,User")]
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
        [Authorize(Roles = "Admin,Manager,User")]
        public IActionResult Create()
        {


            var Exam = _context.Exam.Include(u => u.Subject)
            .Select(s => new
            {
                Text = s.Subject.Title + " | " + s.Date.ToString().Remove(s.Date.ToString().Length - 16),
                Value = s.ExamId

                                }
                                );
            var Student = _context.Student
                                .Select(s => new
                                {
                                    Text = s.FirstName + " " + s.LastName,
                                    Value = s.StudentId

                                }
                                );

            ViewData["ExamId"] = new SelectList(Exam, "Value", "Text");
            ViewData["StudentId"] = new SelectList(Student, "Value", "Text");
          
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeId,Mark,ExamId,StudentId")] Grade grade)
        {

            
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                

            }
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", grade.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", grade.StudentId);
            return View(grade);


        }

        // GET: Grade/Edit/5
        [Authorize(Roles = "Admin,Manager,User")]
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

            var Exam = _context.Exam.Include(u => u.Subject)
                                .Select(s => new
                                {
                                    Text = s.Subject.Title + " | " + s.Date.ToString().Remove(s.Date.ToString().Length - 16),
                                    Value = s.ExamId

                                }
                                );
            var Student = _context.Student
                                .Select(s => new
                                {
                                    Text = s.FirstName + " " + s.LastName,
                                    Value = s.StudentId

                                }
                                );

            ViewData["ExamId"] = new SelectList(Exam, "Value", "Text", grade.ExamId);
            ViewData["StudentId"] = new SelectList(Student, "Value", "Text", grade.StudentId);

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

            if (ModelState.IsValid)
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
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", grade.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", grade.StudentId);
            return View(grade);
        }

        // GET: Grade/Delete/5
        [Authorize(Roles = "Admin,Manager,User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grade == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .Include(g => g.Exam)
                .Include(g => g.Student)
                .Include(g => g.Exam.Subject)
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
