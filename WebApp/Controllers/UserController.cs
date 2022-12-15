using Microsoft.AspNet.Identity.Owin;
using Model;
using Services;
using Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserService userService = new UserService();
        private ApplicationRoleManager _roleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }

        private ApplicationUserManager _userManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
        }
        public ActionResult Index()
        {
            return View(
                userService.GetAll()
                );
        }

        public async Task<ActionResult> Get(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await _userManager.FindByIdAsync(id);
            ViewBag.Roles = _roleManager.Roles.Where(x => x.enabled).OrderBy(x => x.Name).ToList();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public async Task<ActionResult> AddRoleToUser(string Id, string role)
        {
            var roles = await _userManager.GetRolesAsync(Id);

            if (roles.Any())
            {
                throw new Exception("El usuario ya tiene un Rol, se permite uno por usuario");
            }

            await _userManager.AddToRoleAsync(Id, role);
            return RedirectToAction("Index");
        }

        public async Task CreateRoles()
        {
            var roles = new List<ApplicationRol>{
                new ApplicationRol { Name = "Admin"},
                new ApplicationRol { Name = "Empleado"}
            };

            foreach (var r in roles)
            {
                if (!await _roleManager.RoleExistsAsync(r.Name))
                {
                    await _roleManager.CreateAsync(r);
                }
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                //Autenticacion
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);


                    return RedirectToAction("Index", "User");
                }
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        public ActionResult Details(string id)
        {
            var model = userService.GetDetails(id);

            return View("Details", model);
        }

        // GET: Tareas/Delete/5
        public ActionResult Delete(string id)
        {
            var model = userService.GetDetails(id);
            return View(model);
        }
        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                userService.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}