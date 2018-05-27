using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace M120Projekt.DAL
{
    public class Kategorie
    {
        public Kategorie() { }
        [Key]
        public Int64 KategorieId { get; set; }
        [Required]
        public String Name { get; set; }
        public override string ToString()
        {
            return KategorieId.ToString(); // Für bessere Coded UI Test Erkennung
        }

    }
}
