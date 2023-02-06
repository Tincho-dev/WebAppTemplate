using Models;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class LetraService
    {

        public static IEnumerable<Letra> GetAll()
        {
            var result = new List<Letra>();

            using (var db = new ApplicationDbContext())
            {
                //result = db.Letras.ToList(); //Traer todo de una tabla
                result = (
                        from l in db.Letras
                        select l
                     ).OrderBy(x => x.Name).ToList();
            }

            return result;

        }


        public static Letra GetDetails(string id)
        {
            var result = new Letra();

            using (var db = new ApplicationDbContext())
            {
                result = (
                        from l in db.Letras
                        select l
                     ).Single();
            }

            return result;
        }

        public static void Create(Letra Letra)
        {
            using (var db = new ApplicationDbContext())
            {
                var letra = new Letra(Letra.Name ,Letra.Probability,Letra.FrecuenciaDeAparicion,Letra.IdFuente);

                db.Letras.Add(letra);
                db.SaveChanges();
            }
        }

        public static void Delete(string id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    Letra user = db.Letras.Where(x => x.Id == id).Single();

                    db.Letras.Remove(user);
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


