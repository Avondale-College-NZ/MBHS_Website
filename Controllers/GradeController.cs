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
using System.Runtime.Intrinsics.X86;

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

        public async Task<IActionResult> Index(string sortOrder,
   string currentFilter,
   string SearchString,
   int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["GradeSort"] = sortOrder == "Grade" ? "Grade_desc" : "Grade";
            ViewData["SubjectSort"] = sortOrder == "Subject" ? "Subject_desc" : "Subject";
            ViewData["DateSort"] = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewData["StudentSort"] = sortOrder == "Student" ? "Student_desc" : "Student";


            if (_context.Student == null)
            {
                return Problem("Entity set 'MBHS_Website.Grade'  is null.");
            }

            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            var name = from n in _context.Grade.Include(g => g.Exam).Include(g => g.Student).Include(g => g.Exam.Subject)
                       select n;

            if (!System.String.IsNullOrEmpty(SearchString)) //filter feature
            {
                name = name.Where(s => s.Student.LastName!.Contains(SearchString));
            }

            switch (sortOrder)
            {
                case "Grade_desc":
                    name = name.OrderByDescending(s => s.Mark);
                    break;
                case "Grade":
                    name = name.OrderBy(s => s.Mark);
                    break;
                case "Subject_desc":
                    name = name.OrderByDescending(s => s.Exam.Subject.Title);
                    break;
                case "Subject":
                    name = name.OrderBy(s => s.Exam.Subject.Title);
                    break;
                case "Date_desc":
                    name = name.OrderByDescending(s => s.Exam.Date);
                    break;
                case "Date":
                    name = name.OrderBy(s => s.Exam.Date);
                    break;
                case "Student_desc":
                    name = name.OrderByDescending(s => s.Student.FirstName).ThenByDescending(s => s.Student.FirstName);
                    break;
                case "Student":
                    name = name.OrderBy(s => s.Student.FirstName).ThenBy(s => s.Student.FirstName);
                    break;
                default:
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Grade>.CreateAsync(name.AsNoTracking(), pageNumber ?? 1, pageSize));

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

            var SST = _context.StudentSubjectTeacher.Include(v => v.SubjectTeacher).ToList();
            var exams = _context.Exam.ToList();
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

            foreach (var student in SST)
            {
                if (student.StudentId == grade.StudentId)
                {
                    foreach (var e in exams)
                    {
                        if (e.ExamId == grade.ExamId)
                        {

                       
                            if (e.SubjectId == student.SubjectTeacher.SubjectId)
                            {
                                if (ModelState.IsValid)
                                {
                                    if (e.Date < System.DateTime.Now)
                                    {
                                        _context.Add(grade);
                                        await _context.SaveChangesAsync();
                                        return RedirectToAction(nameof(Index));
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("CustomError", "* Chosen exam has not yet been conducted");
                                        ViewData["ExamId"] = new SelectList(Exam, "Value", "Text", grade.ExamId);
                                        ViewData["StudentId"] = new SelectList(Student, "Value", "Text", grade.StudentId);

                                        return View(grade);
                                    }

                                }
                                else
                                {
                              
                                        ViewData["ExamId"] = new SelectList(Exam, "Value", "Text", grade.ExamId);
                                        ViewData["StudentId"] = new SelectList(Student, "Value", "Text", grade.StudentId);

                                        return View(grade);
                             
                                }
                            }
                        }
                    }
                }
            }
            
            ModelState.AddModelError("CustomError", "* Chosen student does not take that particlar subject");
            ViewData["ExamId"] = new SelectList(Exam, "Value", "Text", grade.ExamId);
                    ViewData["StudentId"] = new SelectList(Student, "Value", "Text", grade.StudentId);

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
            var SST = _context.StudentSubjectTeacher.Include(v => v.SubjectTeacher).ToList();
            var exams = _context.Exam.ToList();

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
            if (id != grade.GradeId)
            {
                return NotFound();
            }

           
                   
              
            foreach (var student in SST)
            {
                if (student.StudentId == grade.StudentId)
                {
                    foreach (var e in exams)
                    {
                        if (e.ExamId == grade.ExamId)
                        {


                            if (e.SubjectId == student.SubjectTeacher.SubjectId)
                            {
                                if (ModelState.IsValid)
                                {
                                    if (e.Date < System.DateTime.Now)
                                    {
                                        _context.Update(grade);
                                        await _context.SaveChangesAsync();
                                        return RedirectToAction(nameof(Index));
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("CustomError", "* Chosen exam has not yet been conducted");
                                        ViewData["ExamId"] = new SelectList(Exam, "Value", "Text", grade.ExamId);
                                        ViewData["StudentId"] = new SelectList(Student, "Value", "Text", grade.StudentId);

                                        return View(grade);
                                    }

                                }
                                else
                                {

                                    ViewData["ExamId"] = new SelectList(Exam, "Value", "Text", grade.ExamId);
                                    ViewData["StudentId"] = new SelectList(Student, "Value", "Text", grade.StudentId);

                                    return View(grade);

                                }
                            }
                        }
                    }
                }
            }

            ModelState.AddModelError("CustomError", "* Chosen student does not take that particlar subject");
            ViewData["ExamId"] = new SelectList(Exam, "Value", "Text", grade.ExamId);
            ViewData["StudentId"] = new SelectList(Student, "Value", "Text", grade.StudentId);


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
