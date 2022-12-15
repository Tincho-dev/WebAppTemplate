using Model.Domain;
using Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RolesEmpController : Controller
    {
        private readonly RolesServices _rolServices = new RolesServices();
        // GET: RolesEmp
        public ActionResult Index()
        {
            var model = _rolServices.GetAll();
            return View(model);
        }

        public ActionResult Buscar(string palabra)
        {

            var roles = _rolServices.Buscar(palabra);

            Session["Palabra"] = palabra;

            return View("Index", roles);
        }

        // GET: RolesEmp/Details/5
        public ActionResult Details(int id)
        {
            var model = _rolServices.Get(id);
            return View("Details", model);
        }

        // GET: RolesEmp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesEmp/Create
        [HttpPost]
        public ActionResult Create(RolEmp rol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _rolServices.Create(rol);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: RolesEmp/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _rolServices.Get(id);
            return View(model);
        }

        // POST: RolesEmp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RolEmp rol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _rolServices.update(rol);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: RolesEmp/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _rolServices.Get(id);
            return View(model);
        }

        // POST: RolesEmp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            try
            {

                _rolServices.delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ActionResult Reporte()
        {
            IEnumerable<RolEmp> roles;

            if (Session["Palabra"] != null)
            {
                string palabra = Session["Palabra"].ToString();

                roles = _rolServices.Buscar(palabra);
            }
            else
            {
                roles = _rolServices.GetAll();
            }

            return View("Reporte", roles);
        }
    }
}
