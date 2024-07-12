using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBHS_Website.Controllers
{
    public class NavigationController : Controller
    {
        // GET: NavigationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NavigationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NavigationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NavigationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NavigationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NavigationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NavigationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NavigationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
