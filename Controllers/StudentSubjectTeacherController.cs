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
    public class StudentSubjectTeacherController : Controller
    {
        private readonly MBHS_Context _context;

        public StudentSubjectTeacherController(MBHS_Context context)
        {
            _context = context;
        }

        // GET: StudentSubjectTeacher
        public async Task<IActionResult> Index()
        {
            var mBHS_Context = _context.StudentSubjectTeacher.Include(s => s.Student).Include(s => s.SubjectTeacher).Include(s => s.SubjectTeacher.Subject).Include(s => s.SubjectTeacher.Teacher);
            return View(await mBHS_Context.ToListAsync());
        }

        // GET: StudentSubjectTeacher/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentSubjectTeacher == null)
            {
                return NotFound();
            }

            var studentSubjectTeacher = await _context.StudentSubjectTeacher
                .Include(s => s.Student)
                .Include(s => s.SubjectTeacher)
                .FirstOrDefaultAsync(m => m.StudentSubjectTeacherId == id);
            if (studentSubjectTeacher == null)
            {
                return NotFound();
            }

            return View(studentSubjectTeacher);
        }

        // GET: StudentSubjectTeacher/Create
        public IActionResult Create()
        {

            var SubjectTeacher = _context.SubjectTeacher.Include(u => u.Subject).Include(u => u.Teacher)
                                .Select(s => new
                                {
                                    Text = s.Subject.Title + " | " + s.Teacher.FirstName + " " + s.Teacher.LastName,
                                    Value = s.SubjectTeacherId

                                }
                                );
            ViewData["SubjectTeacherId"] = new SelectList(SubjectTeacher, "Value", "Text");

            var Student = _context.Student
                               .Select(s => new
                               {
                                   Text = s.FirstName + " " + s.LastName,
                                   Value = s.StudentId

                               }
                               );
            ViewData["StudentId"] = new SelectList(Student, "Value", "Text");
            
            return View();
        }

        // POST: StudentSubjectTeacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentSubjectTeacherId,StudentId,SubjectTeacherId")] StudentSubjectTeacher studentSubjectTeacher)
        {
            if (!ModelState.IsValid)

            {
                _context.Add(studentSubjectTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", studentSubjectTeacher.StudentId);
            ViewData["SubjectTeacherId"] = new SelectList(_context.Set<SubjectTeacher>(), "SubjectTeacherId", "SubjectTeacherId", studentSubjectTeacher.SubjectTeacherId);
            return View(studentSubjectTeacher);
        }

        // GET: StudentSubjectTeacher/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentSubjectTeacher == null)
            {
                return NotFound();
            }

            var studentSubjectTeacher = await _context.StudentSubjectTeacher.FindAsync(id);
            if (studentSubjectTeacher == null)
            {
                return NotFound();
            }


            var SubjectTeacher = _context.SubjectTeacher.Include(u => u.Subject).Include(u => u.Teacher)
                               .Select(s => new
                               {
                                   Text = s.Subject.Title + " | " + s.Teacher.FirstName + " " + s.Teacher.LastName,
                                   Value = s.SubjectTeacherId

                               }
                               );
        

            var Student = _context.Student
                               .Select(s => new
                               {
                                   Text = s.FirstName + " " + s.LastName,
                                   Value = s.StudentId

                               }
                               );
          
            ViewData["StudentId"] = new SelectList(Student, "Value", "Text", studentSubjectTeacher.StudentId);
            ViewData["SubjectTeacherId"] = new SelectList(SubjectTeacher, "Value", "Text", studentSubjectTeacher.SubjectTeacherId);
            return View(studentSubjectTeacher);
        }

        // POST: StudentSubjectTeacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentSubjectTeacherId,StudentId,SubjectTeacherId")] StudentSubjectTeacher studentSubjectTeacher)
        {
            if (id != studentSubjectTeacher.StudentSubjectTeacherId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentSubjectTeacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentSubjectTeacherExists(studentSubjectTeacher.StudentSubjectTeacherId))
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
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", studentSubjectTeacher.StudentId);
            ViewData["SubjectTeacherId"] = new SelectList(_context.Set<SubjectTeacher>(), "SubjectTeacherId", "SubjectTeacherId", studentSubjectTeacher.SubjectTeacherId);
            return View(studentSubjectTeacher);
        }

        // GET: StudentSubjectTeacher/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentSubjectTeacher == null)
            {
                return NotFound();
            }

            var studentSubjectTeacher = await _context.StudentSubjectTeacher.Include(s => s.Student).Include(s => s.SubjectTeacher).Include(s => s.SubjectTeacher.Subject).Include(s => s.SubjectTeacher.Teacher)
                .FirstOrDefaultAsync(m => m.StudentSubjectTeacherId == id);
            if (studentSubjectTeacher == null)
            {
                return NotFound();
            }

            return View(studentSubjectTeacher);
        }

        // POST: StudentSubjectTeacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentSubjectTeacher == null)
            {
                return Problem("Entity set 'MBHS_Context.StudentSubjectTeacher'  is null.");
            }
            var studentSubjectTeacher = await _context.StudentSubjectTeacher.FindAsync(id);
            if (studentSubjectTeacher != null)
            {
                _context.StudentSubjectTeacher.Remove(studentSubjectTeacher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentSubjectTeacherExists(int id)
        {
          return (_context.StudentSubjectTeacher?.Any(e => e.StudentSubjectTeacherId == id)).GetValueOrDefault();
        }
    }
}
