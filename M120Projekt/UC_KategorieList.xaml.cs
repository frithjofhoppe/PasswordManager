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
    /// Interaction logic for UC_KategorieList.xaml
    /// </summary>
    public partial class UC_KategorieList : UserControl
    {
        MainWindow parent;

        public UC_KategorieList()
        {
            InitializeComponent();
        }

        public UC_KategorieList(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            ListInit();
        }

        private void ListInit() {
            ListKategorie.CanUserAddRows = false;
            ListKategorie.IsReadOnly = true;
            ListKategorie.CanUserSortColumns = true;
            ListKategorie.SelectionMode = DataGridSelectionMode.Single;
            ListKategorie.AutoGenerateColumns = false;
            ListKategorie.ItemsSource = BLL.Kategorie.LesenAlle().OrderBy(i => i.Name);
        }

        private void ListKategorie_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DAL.Kategorie kategorie = (DAL.Kategorie)(((DataGrid)sender).SelectedItem);
            parent.LoadPasswordListView(kategorie);
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            parent.LoadView(new UC_Category(parent), "New category");
        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
