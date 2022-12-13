using Model.Domain;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Services
{
    public class RolesServices
    {
        private readonly ApplicationDbContext ctx = new ApplicationDbContext();
        public IEnumerable<RolEmp> GetAll()
        {
            var result = new List<RolEmp>();

            using (ctx)
            {
                result = ctx.RolEmpleado.OrderBy(x => x.Nombre).ToList();
            }

            return result;
        }

        public RolEmp Get(int id)
        {
            var result = new RolEmp();

            using (ctx)
            {
                result = ctx.RolEmpleado.Where(x => x.Id_Rol == id).Single();
            }

            return result;
        }

        public void Create(RolEmp rol)
        {
            using (ctx)
            {
                var rolEmp = new RolEmp();

                rolEmp.Nombre = rol.Nombre;

                ctx.RolEmpleado.Add(rolEmp);
                ctx.SaveChanges();
            }
        }

        public void update(RolEmp rol)
        {
            using (ctx)
            {
                var originalEntity = ctx.RolEmpleado.Where(x => x.Id_Rol == rol.Id_Rol).Single();

                originalEntity.Nombre = rol.Nombre;

                ctx.Entry(originalEntity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void delete(int id)
        {
            try
            {
                using (ctx)
                {
                    RolEmp rol = ctx.RolEmpleado.Where(x => x.Id_Rol == id).Single();

                    ctx.RolEmpleado.Remove(rol);

                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<RolEmp> Buscar(string palabra)
        {
            IEnumerable<RolEmp> roles;

            using (var db = new ApplicationDbContext())
            {
                roles = db.RolEmpleado;
                if (!String.IsNullOrEmpty(palabra))
                {
                    roles = roles.Where(x => x.Nombre.ToUpper().Contains(palabra.ToUpper()));
                }

                roles = roles.ToList();
            }

            return roles;
        }
    }
}
