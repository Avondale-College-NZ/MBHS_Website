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
    public class SubjectController : Controller
    {
        private readonly MBHS_Context _context;

        public SubjectController(MBHS_Context context)
        {
            _context = context;
        }

        // GET: Subject
   
        public async Task<IActionResult> Index(string sortOrder,
   string currentFilter,
   string SearchString,
   int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SubjectSort"] = sortOrder == "Subject" ? "Subject_desc" : "Subject";
            ViewData["DepartmentSort"] = sortOrder == "Department" ? "Department_desc" : "Department";
           


            if (_context.Subject == null)
            {
                return Problem("Entity set 'MBHS_Website.SubjectTeacher'  is null.");
            }

            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            var name = from n in _context.Subject.Include(m => m.Department)
                       select n;

            if (!String.IsNullOrEmpty(SearchString)) //filter feature
            {
                name = name.Where(s => s.Title!.Contains(SearchString));
            }

            switch (sortOrder)
            {
                case "Subject_desc":
                    name = name.OrderByDescending(s => s.Title);
                    break;
                case "Subject":
                    name = name.OrderBy(s => s.Title);
                    break;
                case "Department_desc":
                    name = name.OrderByDescending(s => s.Department.Title);
                    break;
                case "Department":
                    name = name.OrderBy(s => s.Department.Title);
                    break;
              
                default:
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Subject>.CreateAsync(name.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Subject/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subject == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Title");
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,Title,DepartmentId")] Subject subject)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentId", subject.DepartmentId);
            return View(subject);
        }

        // GET: Subject/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subject == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Title", subject.DepartmentId);
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectId,Title,DepartmentId")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.SubjectId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentId", subject.DepartmentId);
            return View(subject);
        }

        // GET: Subject/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subject == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subject == null)
            {
                return Problem("Entity set 'MBHS_Context.Subject'  is null.");
            }
            var subject = await _context.Subject.FindAsync(id);
            if (subject != null)
            {
                _context.Subject.Remove(subject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
          return (_context.Subject?.Any(e => e.SubjectId == id)).GetValueOrDefault();
        }
    }
}
