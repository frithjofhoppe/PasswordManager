using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //if(BLL.Passwort.LesenAlle().Count == 0 && BLL.Kategorie.LesenAlle().Count == 0)
            //{
            //    DemoErstellen();
            //}
            //DemoAbfragen();
            initalize();
        }
        #region Demo
        private void DemoErstellen()
        {
            // Kategorie (kurze Syntax)
            DAL.Kategorie kategorie1 = new DAL.Kategorie{ Name = "Kategorie 1"};
            Int64 kategorie1Id = BLL.Kategorie.Erstellen(kategorie1);
            Debug.Print("Kategorie erstellt mit Id:" + kategorie1Id);
            DAL.Kategorie kategorie2 = new DAL.Kategorie { Name = "Kategorie 2"};
            Int64 kategorie2Id = BLL.Kategorie.Erstellen(kategorie2);
            Debug.Print("Kategorie erstellt mit Id:" + kategorie2Id);
            // Passwort (detaillierte Syntax)
            DAL.Passwort passwort1 = new DAL.Passwort();
            passwort1.Login = "vmadmin";
            passwort1.PSW = "gibbiX12345";
            passwort1.Eingabedatum = DateTime.Today;
            passwort1.Ablaufdatum = DateTime.Today.AddMonths(1);
            passwort1.Kategorie = kategorie1;
            Int64 passwort1Id = BLL.Passwort.Erstellen(passwort1);
            Debug.Print("Passwort erstellt mit Id:" + passwort1Id);
        }
        private void DemoAbfragen()
        {
            String output = "";
            // Alle Records Passwort mit Details zu verknüpftem Record aus Kategorie
            output += Environment.NewLine + "Alle Records Passwort";
            foreach (DAL.Passwort classA in BLL.Passwort.LesenAlle())
            {
                output += Environment.NewLine + "Passwort Login:" + classA.Login;
                output += Environment.NewLine + "Passwort Kategorie Name:" + classA.Kategorie.Name;
            }
            output += Environment.NewLine + "------------------------------------------------------";
            // Alle Records Kategorie
            output += Environment.NewLine + "Alle Records Kategorie";
            foreach (DAL.Kategorie classB in BLL.Kategorie.LesenAlle())
            {
                output += Environment.NewLine + "Kategorie Name:" + classB.Name;
            }
            output += Environment.NewLine + "------------------------------------------------------";
            Debug.Print(output);
        }
        #endregion

        private void initalize()
        {
            LoadStartPage();
            ListKateorie.Children.Add(new UC_KategorieList(this));
            MainTitle.Background = Brushes.Gray;
        }

        private void LoadStartPage()
        {
            LoadView(new UC_Startpage(this), "Welcome");
        }

        public void UpdateCategoryList() {
            ListKateorie.Children.Clear();
            ListKateorie.Children.Add(new UC_KategorieList(this));
        }

        public void LoadPasswordListView(DAL.Kategorie kategorie)
        {
            MainTitle.Content = kategorie.Name;
            this.mainShowPlace.Children.Clear();
            this.mainShowPlace.Children.Add(new UC_PasswordListView(this, kategorie));
        }

        public void LoadView(UIElement view, string title)
        {
            MainTitle.Content = title;
            this.mainShowPlace.Children.Clear();
            if (view != null)
            {
                this.mainShowPlace.Children.Add(view);
            }
        }

        private void MenuStartpage_Click(object sender, RoutedEventArgs e)
        {
            LoadStartPage();
        }

        private void MenutExit_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog();
        }

        private void CloseDialog()
        {
            MessageBoxResult mbr = MessageBox.Show("Do you want to quit?", "Closing", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mbr == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            CloseDialog();
        }
    }
}
