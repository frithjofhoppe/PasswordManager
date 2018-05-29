using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace M120Projekt.Additonal
{
    public static class RegexLib
    {
        public static bool IsNameValid(string input)
        {
            if (Regex.IsMatch(input, @"^[a-zA-Z0-9]{3}[a-zA-Z0-9\s]*$")) return true;
            return false;
        }

        public static bool IsPasswordValid(string input)
        {
            if (Regex.IsMatch(input, @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=])(?=\S+$).{8,}$")) return true;
            return false;
        }

        public static bool Match(Func<string, bool> function, string content, Control item)
        {
            if (content.Trim().Length > 0)
            {
                if (function(content))
                {
                    item.Background = Brushes.Green;
                    return true;
                }
                item.Background = Brushes.Red;
                return false;
            }
            item.Background = Brushes.White;
            return false;
        }
    }
}
