using Models;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class FuenteService
    {

        public static IEnumerable<Fuente> GetAll()
        {
            var result = new List<Fuente>();

            using (var db = new ApplicationDbContext())
            {
                //result = db.Fuentes.ToList(); //Traer todo de una tabla
                result = (
                        from l in db.Fuentes
                        select l
                     ).ToList();
            }

            return result;

        }


        public static Fuente GetDetails(string id)
        {
            var result = new Fuente();

            using (var db = new ApplicationDbContext())
            {
                result = (
                        from f in db.Fuentes.Where(x=>x.IdFuente == id)
                        select f
                     ).Single();
            }

            return result;
        }

        public static void Create(Fuente Fuente)
        {
            using (var db = new ApplicationDbContext())
            {
                var fuente = new Fuente(Fuente.CadenaFuente);
                db.Fuentes.Add(fuente);
                db.SaveChanges();
                foreach (var Letra in fuente.Letras)
                {
                    LetraService.Create(Letra);
                }
            }
        }

        public static void Delete(string id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    Fuente user = db.Fuentes.Where(x => x.IdFuente == id).Single();

                    db.Fuentes.Remove(user);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Update(Fuente fuente)
        {
            throw new NotImplementedException();
        }
    }
}


