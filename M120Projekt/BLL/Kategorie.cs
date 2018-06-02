using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M120Projekt.BLL
{
    static class Kategorie
    {
        public static bool Existiert(String name)
        {
            using (var context = new DAL.Context())
            {
                var catergory = context.Kategorie
                    .FirstOrDefault(i => i.Name.Trim().ToLower() == name.Trim().ToLower());
                return catergory != null;
            }
        }

        public static List<DAL.Kategorie> LesenAlle()
        {
            using (var context = new DAL.Context())
            {
                return (from record in context.Kategorie select record).ToList();
            }
        }
        public static DAL.Kategorie LesenID(Int64 passwortId)
        {
            using (var context = new DAL.Context())
            {
                return (from record in context.Kategorie where record.KategorieId == passwortId select record).FirstOrDefault();
            }
        }
        public static List<DAL.Kategorie> LesenAttributGleich(String suchbegriff)
        {
            using (var context = new DAL.Context())
            {
                return (from record in context.Kategorie where record.Name == suchbegriff select record).ToList();
            }
        }
        public static List<DAL.Kategorie> LesenAttributWie(String suchbegriff)
        {
            using (var context = new DAL.Context())
            {
                return (from record in context.Kategorie where record.Name.Contains(suchbegriff) select record).ToList();
            }
        }
        //public static List<DAL.ClassB> LesenFremdschluesselGleich(DAL.ClassA suchschluessel)
        //{
        //    using (var context = new DAL.Context())
        //    {
        //        return (from record in context.ClassA where record.FremdschluesselAttribut == suchschluessel select record).ToList();
        //    }
        //}
        public static Int64 Erstellen(DAL.Kategorie kategorie)
        {
            if (kategorie.Name == null || kategorie.Name == "") kategorie.Name = "leer";
            using (var context = new DAL.Context())
            {
                context.Kategorie.Add(kategorie);
                context.SaveChanges();
                return kategorie.KategorieId;
            }
        }
        public static void Aktualisieren(DAL.Kategorie kategorie)
        {
            using (var context = new DAL.Context())
            {
                context.Entry(kategorie).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void LoeschenById(long id)
        {
            using(var context = new DAL.Context())
            {
                var category = context.Kategorie.FirstOrDefault(i => i.KategorieId == id);
                if(category != null)
                {
                    context.Kategorie.Remove(category);
                    context.SaveChanges();
                }
            }
        }

        public static void Loeschen(DAL.Kategorie kategorie)
        {
            using (var context = new DAL.Context())
            {
                context.Kategorie.Remove(kategorie);
                context.SaveChanges();
            }
        }
    }
}
