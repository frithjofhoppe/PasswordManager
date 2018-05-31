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
    /// Interaction logic for UC_PasswordListView.xaml
    /// </summary>
    public partial class UC_PasswordListView : UserControl
    {
        MainWindow parent;
        DAL.Passwort currentPassword;
        DAL.Kategorie currentCategory;
        public UC_PasswordListView(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            initialize();
        }

        public UC_PasswordListView(MainWindow parent, DAL.Kategorie kategorie)
        {
            InitializeComponent();
            this.parent = parent;
            this.currentCategory = kategorie;
            LoadView(kategorie);
            initialize();
        }

        private void initialize()
        {
            //BTNDelete.Visibility = Visibility.Hidden;
        }

        private void LoadView(DAL.Kategorie kategorie)
        {
            ListPasswords.CanUserAddRows = false;
            ListPasswords.IsReadOnly = true;
            ListPasswords.CanUserSortColumns = true;
            ListPasswords.SelectionMode = DataGridSelectionMode.Single;
            ListPasswords.AutoGenerateColumns = false;
            ListPasswords.ItemsSource = BLL.Passwort.LesenFremdschluesselGleich(kategorie);
        }

        private void ListPasswords_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            parent.LoadView(new UC_Password(parent, currentPassword, Additonal.WorkingStatus.LOADED), "Edit password");
        }

        private void ListPasswords_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            currentPassword = (DAL.Passwort)(((DataGrid)sender).SelectedItem);
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            parent.LoadView(new UC_Password(parent, Additonal.WorkingStatus.NEW), "New password");
        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
            if(currentPassword != null)
            {
                MessageBoxResult mbr = MessageBox.Show("Do you realy want to delete the password", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (mbr == MessageBoxResult.Yes)
                {
                    BLL.Passwort.LoeschenById(currentPassword.PasswortId);
                    MessageBox.Show("Password has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    parent.LoadView(new UC_PasswordListView(parent, currentCategory), "New password");
                }
            }
        }
    }
}
