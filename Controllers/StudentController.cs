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
    public class StudentController : Controller
    {
        private readonly MBHS_Context _context;

        public StudentController(MBHS_Context context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string SearchString,
    int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["FirstNameSort"] = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewData["LastNameSort"] = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewData["DateOfBirthSort"] = sortOrder == "DateOfBirth" ? "DateOfBirth_desc" : "DateOfBirth";


            if (_context.Student == null)
            {
                return Problem("Entity set 'MBHS_Website.Students'  is null.");
            }

            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            var name = from n in _context.Student
                       select n;

            if (!String.IsNullOrEmpty(SearchString)) //filter feature
            {
                name = name.Where(s => s.LastName!.Contains(SearchString));
            }

            switch (sortOrder)
            {
                case "FirstName_desc":
                    name = name.OrderByDescending(s => s.FirstName);
                    break;
                case "FirstName":
                    name = name.OrderBy(s => s.FirstName);
                    break;
                case "LastName_desc":
                    name = name.OrderByDescending(s => s.LastName);
                    break;
                case "LastName":
                    name = name.OrderBy(s => s.LastName);
                    break;
                case "DateOfBirth_desc":
                    name = name.OrderByDescending(s => s.DateOfBirth);
                    break;
                case "DateOfBirth":
                    name = name.OrderBy(s => s.DateOfBirth);
                    break;
                default:
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Student>.CreateAsync(name.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Students/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [Authorize(Roles = "Admin,Manager")]
        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,DateOfBirth")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,DateOfBirth")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            return View(student);
        }


        // GET: Students/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'MBHS_Context.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Student?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
