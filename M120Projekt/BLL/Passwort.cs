using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace M120Projekt.BLL
{
    static class Passwort
    {
        public static int IstAbgelaufen()
        {
            using (var context = new DAL.Context())
            {
                return context.Passwort
                    .Where(i => i.Ablaufdatum <= DateTime.Today)
                    .Count();
            }
        }

        public static List<DAL.Passwort> ZielsystemWie(string searchTerm, long categoryid)
        {
            using (var context = new DAL.Context())
            {
                return context.Passwort
                    .Include(i => i.Kategorie)
                    .Where(i => i.Kategorie.KategorieId == categoryid)
                    .Where(i => i.Zielsystem.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }
        }

        public static List<DAL.Passwort> BenutzerWie(string searchTerm, long categoryid)
        {
            using (var context = new DAL.Context())
            {
                return context.Passwort
                    .Include(i => i.Kategorie)
                    .Where(i => i.Kategorie.KategorieId == categoryid)
                    .Where(i => i.Login.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }
        }

        public static List<DAL.Passwort> LesenAlleAbgelaufen()
        {
            using (var context = new DAL.Context())
            {
                return context.Passwort
                    .Include(i => i.Kategorie)
                    .Where(i => i.Ablaufdatum <= DateTime.Today)
                    .ToList();
            }
        }

        public static List<DAL.Passwort> LesenAlle()
        {
            using (var context = new DAL.Context())
            {
                return (from record in context.Passwort.Include("Kategorie") select record).ToList();
            }
        }
        public static DAL.Passwort LesenID(Int64 passwortId)
        {
            using (var context = new DAL.Context())
            {
                return (from record in context.Passwort.Include("Kategorie") where record.PasswortId == passwortId select record).FirstOrDefault();
            }
        }
        public static List<DAL.Passwort> LesenAttributGleich(String suchbegriff)
        {
            using (var context = new DAL.Context())
            {
                return (from record in context.Passwort.Include("Kategorie") where record.Login == suchbegriff select record).ToList();
            }
        }
        public static List<DAL.Passwort> LesenAttributWie(String suchbegriff)
        {
            using (var context = new DAL.Context())
            {
                return (from record in context.Passwort.Include("Kategorie") where record.Zielsystem.Contains(suchbegriff) select record).ToList();
            }
        }
        public static List<DAL.Passwort> LesenFremdschluesselGleich(DAL.Kategorie suchschluessel)
        {
            using (var context = new DAL.Context())
            {
                return (from record in context.Passwort.Include("Kategorie") where record.Kategorie.KategorieId == suchschluessel.KategorieId select record).ToList();
            }
        }
        public static Int64 Erstellen(DAL.Passwort passwort)
        {
            if (passwort.Login == null || passwort.Login == "") passwort.Login = "leer";
            if (passwort.Zielsystem == null || passwort.Zielsystem == "") passwort.Zielsystem = "leer";
            if (passwort.PSW == null || passwort.PSW == "") passwort.PSW = "leer";
            // if (classA.TextAttribut == null) throw new Exception("Null ist ungültig");
            if (passwort.Eingabedatum == null) passwort.Eingabedatum = DateTime.MinValue;
            if (passwort.Ablaufdatum == null) passwort.Ablaufdatum = DateTime.MinValue;
            using (var context = new DAL.Context())
            {
                context.Passwort.Add(passwort);
                if (passwort.Kategorie != null) context.Kategorie.Attach(passwort.Kategorie);
                context.SaveChanges();
                return passwort.PasswortId;
            }
        }
        public static void Aktualisieren(DAL.Passwort passwort)
        {
            using (var context = new DAL.Context())
            {
                context.Entry(passwort).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            using (var context = new DAL.Context())
            {
                var password = context.Passwort
                    .Include(i => i.Kategorie)
                    .FirstOrDefault(i => i.PasswortId == passwort.PasswortId);

                var category = context.Kategorie
                    .FirstOrDefault(i => i.KategorieId == passwort.Kategorie.KategorieId);

                if (category != null)
                {
                    context.Entry(password.Kategorie).State = EntityState.Modified;
                    context.Entry(password).State = EntityState.Modified;
                    password.Kategorie = category;
                    context.SaveChanges();
                }
            }
        }

        public static void LoeschenById(long id)
        {
            using(var context = new DAL.Context())
            {
                var pw = context.Passwort.FirstOrDefault(i => i.PasswortId == id);
                if(pw != null)
                {
                    context.Passwort.Remove(pw);
                    context.SaveChanges();
                }
            }
        }

        public static void Loeschen(DAL.Passwort passwort)
        {
            using (var context = new DAL.Context())
            {
                context.Entry(passwort).State = System.Data.Entity.EntityState.Deleted;
                context.Passwort.Remove(passwort);
                context.SaveChanges();
            }
        }
    }
}
