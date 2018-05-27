using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace M120Projekt.DAL
{
    public class Context : DbContext
    {
        public Context() : base("name=M120Connectionstring")
        {
            this.Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DAL.Context, M120Projekt.Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passwort>().ToTable("Passwort"); // Damit kein "s" angehängt wird an Tabelle
            modelBuilder.Entity<Kategorie>().ToTable("Kategorie"); // Damit kein "s" angehängt wird an Tabelle
        }
        public DbSet<Passwort> Passwort { get; set; }
        public DbSet<Kategorie> Kategorie { get; set; }
    }
}
