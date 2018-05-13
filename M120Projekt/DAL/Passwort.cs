using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace M120Projekt.DAL
{
    class Passwort
    {
        public Passwort() { }
        [Key]
        public Int64 PasswortId { get; set; }
        [Required]
        public String Login { get; set; }
        [Required]
        public String PSW { get; set; }
        [Required]
        public String Zielsystem { get; set; }
        [Required]
        public DateTime Eingabedatum { get; set; }
        [Required]
        public DateTime Ablaufdatum { get; set; }
        [Required]
        public Kategorie Kategorie { get; set; }
        [NotMapped]
        public Boolean Abgelaufen {
            get
            {
                return Ablaufdatum < DateTime.Today;
            }
        }
        public override string ToString()
        {
            return PasswortId.ToString(); // Für bessere Coded UI Test Erkennung
        }
    }
}
