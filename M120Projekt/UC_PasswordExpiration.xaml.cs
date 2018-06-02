using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for UC_PasswordExpiration.xaml
    /// </summary>
    public partial class UC_PasswordExpiration : UserControl
    {
        MainWindow parent;
        DAL.Passwort currentPassword;

        public UC_PasswordExpiration()
        {
            InitializeComponent();
        }

        public UC_PasswordExpiration(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            ListInit();
        }

        private void ListInit()
        {
            ListExpiredPassword.CanUserAddRows = false;
            ListExpiredPassword.IsReadOnly = true;
            ListExpiredPassword.CanUserSortColumns = true;
            ListExpiredPassword.SelectionMode = DataGridSelectionMode.Single;
            ListExpiredPassword.AutoGenerateColumns = false;
            ListExpiredPassword.ItemsSource = BLL.Passwort.LesenAlleAbgelaufen();
        }

        private void ListExpiredPassword_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (currentPassword != null)
            {
                parent.LoadView(new UC_Password(parent, currentPassword, Additonal.WorkingStatus.LOADED), currentPassword.Login);
            }
        }

        private void ListExpiredPassword_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            currentPassword = (DAL.Passwort)(((DataGrid)sender).SelectedItem);
        }
    }

    public class WidthValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var size = (long)value;
                return size / 3;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var size = (long)value;
                return size / 3;
            }
            return 0;
        }
    }
}
