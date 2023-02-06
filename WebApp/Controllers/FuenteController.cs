using Models;
using Services;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class FuenteController : Controller
    {
        // GET: Fuente
        [Authorize]
        public ActionResult Index()
        {
            var listado = FuenteService.GetAll();

            return View(listado);
        }


        // GET: Fuente/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            var model = FuenteService.GetDetails(id);
            return View("Details", model);
        }

        // GET: Fuente/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fuente/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Fuente Fuente)
        {
            if (ModelState.IsValid)
            {
                FuenteService.Create(Fuente);
                return RedirectToAction("Index");
            }

            //ViewBag.FuenteDNI = new SelectList(FuenteService.GetDetailsAll(), "DNI", "ApyNom", proyecto.Fuente_DNI);

            return View(Fuente);
        }


        // GET: Fuente/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            var model = FuenteService.GetDetails(id);
            //ViewBag.DNI = new SelectList(FuenteService.GetDetailsAll(), "DNI", "ApyNom");
            return View(model);
        }

        // POST: Fuente/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Fuente Fuente)
        {

            if (ModelState.IsValid)
            {
                FuenteService.Update(Fuente);
                return RedirectToAction("Index");
            }

            return View(Fuente);
        }

        // GET: Fuente/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            var model = FuenteService.GetDetails(id);
            return View(model);
        }

        // POST: Fuente/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FuenteService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
