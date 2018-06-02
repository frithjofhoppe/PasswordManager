using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M120Projekt
{
    /// <summary>
    /// Interaction logic for UC_Startpage.xaml
    /// </summary>
    public partial class UC_Startpage : UserControl
    {
        MainWindow parent;

        public UC_Startpage()
        {
            InitializeComponent();
        }

        public UC_Startpage(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            Initalize();
            InitStatus();
        }

        private void InitStatus()
        {
            var status = BLL.Passwort.IstAbgelaufen();

            Image myImage3 = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();

            if (status > 0)
            {
                var text = status > 1 ? " passwords" : " password";
                text += " expired";
                TXTStatus.Content = status.ToString() + text;
                bi3.UriSource = new Uri("Ressource/Image/warning.png", UriKind.Relative);
                bi3.EndInit();
                IMGStatus.Source = bi3;
            }
            else
            {
                TXTStatus.Content = "No password expired";
                bi3.UriSource = new Uri("Ressource/Image/success.png", UriKind.Relative);
                bi3.EndInit();
                IMGStatus.Source = bi3;
            }
        }

        private void Initalize()
        {
            GridTopArea.Children.Clear();
            GridTopArea.Children.Add(new UC_PasswordExpiration(parent));
        }
    }
}
