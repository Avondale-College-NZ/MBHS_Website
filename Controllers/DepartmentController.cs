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
    public class DepartmentController : Controller
    {
        private readonly MBHS_Context _context;

        public DepartmentController(MBHS_Context context)
        {
            _context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string SearchString,
    int? pageNumber)
        {
            //sortorders that connect the controller sort order to the view
            ViewData["CurrentSort"] = sortOrder;
            ViewData["BuildingSort"] = sortOrder == "Building" ? "Building_desc" : "Building";
            ViewData["DepartmentSort"] = sortOrder == "Department" ? "Department_desc" : "Department";



            if (_context.Subject == null)
            {
                return Problem("Entity set 'MBHS_Website.SubjectTeacher'  is null.");
            }
            //Filtering functionality
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            var name = from n in _context.Department
                       select n;

            if (!String.IsNullOrEmpty(SearchString)) //filter feature
            {
                name = name.Where(s => s.Title!.Contains(SearchString));
            }

            switch (sortOrder)
            {
                case "Building_desc":
                    name = name.OrderByDescending(s => s.Building);
                    break;
                case "Building":
                    name = name.OrderBy(s => s.Building);
                    break;
                case "Department_desc":
                    name = name.OrderByDescending(s => s.Title);
                    break;
                case "Department":
                    name = name.OrderBy(s => s.Title);
                    break;

                default:
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Department>.CreateAsync(name.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Department/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }


        [Authorize(Roles = "Admin")]
        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,Title,Building")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,Title,Building")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
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
            return View(department);
        }

        // GET: Department/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Department == null)
            {
                return Problem("Entity set 'MBHS_Context.Department'  is null.");
            }
            var department = await _context.Department.FindAsync(id);
            if (department != null)
            {
                _context.Department.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
          return (_context.Department?.Any(e => e.DepartmentId == id)).GetValueOrDefault();
        }
    }
}
