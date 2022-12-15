using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HistorialRolesController : Controller
    {
        // GET: HistorialRoles
        public ActionResult Index()
        {
            return View();
        }

        // GET: HistorialRoles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HistorialRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistorialRoles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HistorialRoles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HistorialRoles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HistorialRoles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HistorialRoles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
