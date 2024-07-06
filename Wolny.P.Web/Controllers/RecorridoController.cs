using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wolny.P.Web.Controllers
{
    public class RecorridoController : Controller
    {
        // GET: RecorridoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RecorridoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecorridoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecorridoController/Create
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

        // GET: RecorridoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecorridoController/Edit/5
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

        // GET: RecorridoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecorridoController/Delete/5
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
