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
    public class SubjectTeacherController : Controller
    {
        private readonly MBHS_Context _context;

        public SubjectTeacherController(MBHS_Context context)
        {
            _context = context;
        }

        // GET: SubjectTeacher
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string SearchString,
    int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["RoomSort"] = sortOrder == "Room" ? "Room_desc" : "Room";
            ViewData["NameSort"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["SubjectSort"] = sortOrder == "Subject" ? "Subject_desc" : "Subject";


            if (_context.SubjectTeacher == null)
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

            var name = from n in _context.SubjectTeacher.Include(m => m.Teacher).Include(m => m.Subject)
                       select n;

            if (!String.IsNullOrEmpty(SearchString)) //filter feature
            {
                name = name.Where(s => s.Subject.Title!.Contains(SearchString));
            }

            switch (sortOrder)
            {
                case "Room_desc":
                    name = name.OrderByDescending(s => s.Room);
                    break;
                case "Room":
                    name = name.OrderBy(s => s.Room);
                    break;
                case "Name_desc":
                    name = name.OrderByDescending(s => s.Teacher.FirstName).ThenByDescending(s => s.Teacher.LastName);
                    break;
                case "Name":
                    name = name.OrderBy(s => s.Teacher.FirstName).ThenBy(s => s.Teacher.LastName);
                    break;
                case "Subject_desc":
                    name = name.OrderByDescending(s => s.Subject.Title);
                    break;
                case "Subject":
                    name = name.OrderBy(s => s.Subject.Title);
                    break;
                default:
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<SubjectTeacher>.CreateAsync(name.AsNoTracking(), pageNumber ?? 1, pageSize));

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
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {

            var Teacher = _context.Users
                                .Select(s => new
                                {
                                    Text = s.FirstName + " " + s.LastName,
                                    Value = s.Id

                                }
                                );
            

            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "Title");
            ViewData["TeacherId"] = new SelectList(Teacher, "Value", "Text");
            //ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: SubjectTeacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectTeacherId,SubjectId,TeacherId,Room")] SubjectTeacher subjectTeacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var Teacher = _context.Users
                                .Select(s => new
                                {
                                    Text = s.FirstName + " " + s.LastName,
                                    Value = s.Id

                                }
                                );



            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "Title", subjectTeacher.SubjectId);

            //ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["TeacherId"] = new SelectList(Teacher, "Value", "Text", subjectTeacher.TeacherId);
            return View(subjectTeacher);
        }

        // GET: SubjectTeacher/Edit/5
        [Authorize(Roles = "Admin,Manager")]
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

            var Teacher = _context.Users
                               .Select(s => new
                               {
                                   Text = s.FirstName + " " + s.LastName,
                                   Value = s.Id

                               }
                               );



            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "Title", subjectTeacher.SubjectId);

            //ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["TeacherId"] = new SelectList(Teacher, "Value", "Text", subjectTeacher.TeacherId);
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

            if (ModelState.IsValid)
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
            var Teacher = _context.Users
                                .Select(s => new
                                {
                                    Text = s.FirstName + " " + s.LastName,
                                    Value = s.Id

                                }
                                );



            ViewData["SubjectId"] = new SelectList(_context.Subject, "SubjectId", "Title", subjectTeacher.SubjectId);

            //ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["TeacherId"] = new SelectList(Teacher, "Value", "Text", subjectTeacher.TeacherId);
            return View(subjectTeacher);
        }

        // GET: SubjectTeacher/Delete/5
        [Authorize(Roles = "Admin,Manager")]
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
