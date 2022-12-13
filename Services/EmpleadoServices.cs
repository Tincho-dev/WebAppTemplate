using Model.Custom;
using Model.Domain;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Services
{
    public class EmpleadoServices
    {
        private readonly UserService userService = new UserService();
            
        public IEnumerable<EmpleadoGrid> GetAll()
        {
            var result = new List<EmpleadoGrid>();
            using (var db = new ApplicationDbContext())
            {
                result = (
                        from emp in db.Empleado
                        from uxp in db.UserPorEmp.Where(x => x.Legajo == emp.Legajo).DefaultIfEmpty()
                        from us in db.ApplicationUsers.Where(x => x.Id == uxp.IdUsuario).DefaultIfEmpty()
                        select new EmpleadoGrid
                        {
                            LegajoEmpleado = emp.Legajo,
                            DNI = emp.DNI,
                            ApyNom = emp.Nombre + " " + emp.Apellido,
                            FechaNacimiento = emp.FechaNacimiento,
                            Usuario = us.Email,
                        }
                    ).OrderBy(x => x.FechaNacimiento).ToList();
            }

            return result;
        }

        public void Create(Empleado model)
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.Empleado.Any(x => x.DNI == model.DNI))
                {
                    throw new Exception("Ya existe un empleado con DNI " + model.DNI);
                }
                var empleado = new Empleado();

                empleado.Nombre = model.Nombre;
                empleado.Apellido = model.Apellido;
                empleado.DNI = model.DNI;
                empleado.FechaNacimiento = model.FechaNacimiento;
                empleado.FechaIngreso = DateTime.Now;

                db.Empleado.Add(empleado);
                db.SaveChanges();
            }
        }

        public EmpleadoGrid Get(int legajo)
        {
            var result = new EmpleadoGrid();

            using (var db = new ApplicationDbContext())
            {
                result = (
                        from emp in db.Empleado.Where(x => x.Legajo == legajo)
                        from uxp in db.UserPorEmp.Where(x => x.Legajo == emp.Legajo).DefaultIfEmpty()
                        from us in db.ApplicationUsers.Where(x => x.Id == uxp.IdUsuario).DefaultIfEmpty()
                        select new EmpleadoGrid
                        {
                            LegajoEmpleado = emp.Legajo,
                            DNI = emp.DNI,
                            ApyNom = emp.Nombre + " " + emp.Apellido,
                            FechaNacimiento = emp.FechaNacimiento,
                            Usuario = us.Email,
                        }
                    ).Single();
            }

            return result;
        }

        public Empleado GetEdit(int legajo)
        {
            var result = new Empleado();

            using (var db = new ApplicationDbContext())
            {
                result = db.Empleado.Where(x => x.Legajo == legajo).Single();
            }

            return result;
        }

        public void Update(Empleado model)
        {
            var result = new List<Empleado>();

            using (var db = new ApplicationDbContext())
            {
                var originalEntity = db.Empleado.Where(x => x.Legajo == model.Legajo).Single();

                originalEntity.Nombre = model.Nombre;
                originalEntity.Apellido = model.Apellido;
                originalEntity.FechaNacimiento = model.FechaNacimiento;

                db.Entry(originalEntity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int legajo)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    Empleado empleado = db.Empleado.Where(x => x.Legajo == legajo).Single();

                    db.Empleado.Remove(empleado);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public UserPorEmp GetUser(int legajo)
        {
            var result = new UserPorEmp();

            using (var ctx = new ApplicationDbContext())
            {
                result = ctx.UserPorEmp.Where(x => x.Legajo == legajo).FirstOrDefault();
            }

            return result;
        }

        public void AddUserToEmp(int legajo, string userid)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.UserPorEmp.Add(new UserPorEmp
                    {
                        Legajo = legajo,
                        IdUsuario = userid
                    });
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<EmpleadoGrid> Buscar(string palabra)
        {
            IEnumerable<EmpleadoGrid> empleado;

            using (var db = new ApplicationDbContext())
            {
                empleado = GetAll();
                if (!String.IsNullOrEmpty(palabra))
                {
                    empleado = from emp in db.Empleado.Where(x => x.Nombre.ToUpper().Contains(palabra.ToUpper())
                                || x.Apellido.ToUpper().Contains(palabra.ToUpper()))
                               from uxp in db.UserPorEmp.Where(x => x.Legajo == emp.Legajo).DefaultIfEmpty()
                               from us in db.ApplicationUsers.Where(x => x.Id == uxp.IdUsuario).DefaultIfEmpty()
                               select new EmpleadoGrid
                               {
                                   LegajoEmpleado = emp.Legajo,
                                   DNI = emp.DNI,
                                   ApyNom = emp.Nombre + " " + emp.Apellido,
                                   FechaNacimiento = emp.FechaNacimiento,
                                   Usuario = us.Email,
                               };
                }

                empleado = empleado.ToList();
            }

            return empleado;
        }
             
    }
}
