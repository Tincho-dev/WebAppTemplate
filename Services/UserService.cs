using Model;
using Model.Custom;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Services
{
    public class UserService
    {
        public IEnumerable<UserGrid> GetAll()
        {
            var result = new List<UserGrid>();

            using (var ctx = new ApplicationDbContext())
            {
                //result = ctx.ApplicationUsers.ToList(); //Traer todo de una tabla
                result = (
                        from au in ctx.ApplicationUsers
                        from aur in ctx.ApplicationUserRoles.Where(x => x.UserId == au.Id).DefaultIfEmpty()
                        from ar in ctx.ApplicationRole.Where(x => x.Id == aur.RoleId && x.enabled).DefaultIfEmpty()
                        select new UserGrid
                        {
                            Id = au.Id,
                            Name = au.Name,
                            LastName = au.LastName,
                            Email = au.Email,
                            Role = ar.Name
                        }
                     ).OrderBy(x => x.Name).ToList();
            }

            return result;
        }

        public UserGrid GetDetails(string id)
        {
            var result = new UserGrid();

            using (var ctx = new ApplicationDbContext())
            {
                result = (
                        from au in ctx.ApplicationUsers.Where(x => x.Id == id)
                        from aur in ctx.ApplicationUserRoles.Where(x => x.UserId == au.Id).DefaultIfEmpty()
                        from ar in ctx.ApplicationRole.Where(x => x.Id == aur.RoleId && x.enabled).DefaultIfEmpty()
                        select new UserGrid
                        {
                            Id = au.Id,
                            Name = au.Name,
                            LastName = au.LastName,
                            Email = au.Email,
                            Role = ar.Name
                        }
                     ).Single();
            }

            return result;
        }

        public void Update(ApplicationUser model)
        {
            var result = new List<ApplicationUser>();

            using (var ctx = new ApplicationDbContext())
            {
                var originalEntity = ctx.ApplicationUsers.Where(x => x.Id == model.Id).Single();

                originalEntity.Name = model.Name;
                originalEntity.LastName = model.LastName;

                ctx.Entry(originalEntity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    ApplicationUser user = db.ApplicationUsers.Where(x => x.Id == id).Single();

                    db.ApplicationUsers.Remove(user);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}