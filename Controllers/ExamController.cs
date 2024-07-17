using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBHS_Website.Areas.Identity.Data;
using MBHS_Website.Models;
using Microsoft.AspNetCore.Authorization;

namespace MBHS_Website.Controllers
{
    public class ExamController : Controller
    {
        private readonly MBHS_Context _context;

        public ExamController(MBHS_Context context)
        {
            _context = context;
        }

        // GET: Exam
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string SearchString,
    int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
           
            ViewData["SubjectSort"] = sortOrder == "Subject" ? "Subject_desc" : "Subject";
            ViewData["DateSort"] = sortOrder == "Date" ? "Date_desc" : "Date";
           


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

            var name = from n in _context.Exam.Include(g => g.Subject)
                       select n;

            if (!System.String.IsNullOrEmpty(SearchString)) //filter feature
            {
                name = name.Where(s => s.Subject.Title!.Contains(SearchString));
            }

            switch (sortOrder)
            {
               
                case "Subject_desc":
                    name = name.OrderByDescending(s => s.Subject.Title);
                    break;
                case "Subject":
                    name = name.OrderBy(s => s.Subject.Title);
                    break;
                case "Date_desc":
                    name = name.OrderByDescending(s => s.Date);
                    break;
                case "Date":
                    name = name.OrderBy(s => s.Date);
                    break;
                default:
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Exam>.CreateAsync(name.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Exam/Details/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exam == null)
            {
                return NotFound();
            }

            var exam = await _context.Exam
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.ExamId == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exam/Create
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Set<Subject>(), "SubjectId", "Title");
            return View();
        }

        // POST: Exam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExamId,SubjectId,Date")] Exam exam)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Set<Subject>(), "SubjectId", "SubjectId", exam.SubjectId);
            return View(exam);
        }

        // GET: Exam/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exam == null)
            {
                return NotFound();
            }

            var exam = await _context.Exam.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Set<Subject>(), "SubjectId", "Title", exam.SubjectId);
            return View(exam);
        }

        // POST: Exam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExamId,SubjectId,Date")] Exam exam)
        {
            if (id != exam.ExamId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.ExamId))
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
            ViewData["SubjectId"] = new SelectList(_context.Set<Subject>(), "SubjectId", "SubjectId", exam.SubjectId);
            return View(exam);
        }

        // GET: Exam/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exam == null)
            {
                return NotFound();
            }

            var exam = await _context.Exam
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.ExamId == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exam == null)
            {
                return Problem("Entity set 'MBHS_Context.Exam'  is null.");
            }
            var exam = await _context.Exam.FindAsync(id);
            if (exam != null)
            {
                _context.Exam.Remove(exam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
          return (_context.Exam?.Any(e => e.ExamId == id)).GetValueOrDefault();
        }
    }
}
