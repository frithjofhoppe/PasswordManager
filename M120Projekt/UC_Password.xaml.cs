using M120Projekt.Additonal;
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
    /// Interaction logic for UC_Password.xaml
    /// </summary>
    public partial class UC_Password : UserControl
    {
        EntityStatus entityStatus;
        WorkingStatus workingStatus;
        MainWindow parent;
        DAL.Passwort currentPassword;
        public UC_Password()
        {
            InitializeComponent();
        }

        public UC_Password(MainWindow parent, WorkingStatus workingStatus)
        {
            InitializeComponent();
            this.parent = parent;
            this.workingStatus = workingStatus;
            this.entityStatus = EntityStatus.UNATTACHED;
            Initialize();
        }

        public UC_Password(MainWindow parent, DAL.Passwort password, WorkingStatus workingStatus)
        {
            InitializeComponent();
            this.parent = parent;
            this.workingStatus = workingStatus;
            this.entityStatus = EntityStatus.UNCHANGED;
            this.currentPassword = password;
            LoadValues(currentPassword);
            Initialize();
        }

        private void Initialize()
        {
            CMBCategory.ItemsSource = BLL.Kategorie.LesenAlle();
            CMBCategory.DisplayMemberPath = "Name";
            CMBCategory.SelectedValuePath = "KategorieId";

            switch (workingStatus)
            {
                case WorkingStatus.NEW: InitalizeNewStatus(); break;
                case WorkingStatus.LOADED: InitalizeLoadedStatus(); break;
            }
        }

        private void InitalizeNewStatus()
        {
            DATECreationDate.IsEnabled = false;
            DATECreationDate.SelectedDate = DateTime.Now;
            DATECreationDate.DisplayDateStart = DateTime.Now;
            DATEExpirationDate.DisplayDateStart = DATECreationDate.SelectedDate.Value.Date.AddDays(1);
            DATEExpirationDate.SelectedDate = DATECreationDate.SelectedDate.Value.Date.AddDays(1);
            currentPassword = new DAL.Passwort()
            {
                Kategorie = null,
                Eingabedatum = DATECreationDate.SelectedDate.Value,
                Ablaufdatum = DATEExpirationDate.SelectedDate.Value,
                PSW = "",
                Zielsystem = "",
                Login = "",
            };
        }

        private void InitalizeLoadedStatus()
        {
            CMBCategory.IsEnabled = false;
            DATECreationDate.IsEnabled = false;
            DATEExpirationDate.DisplayDateStart = DATECreationDate.SelectedDate.Value.Date.AddDays(1);
        }

        private void LoadValues(DAL.Passwort pw)
        {
            TXTName.Text = pw.Zielsystem;
            DATECreationDate.SelectedDate = pw.Eingabedatum;
            DATEExpirationDate.SelectedDate = pw.Ablaufdatum;
            CMBCategory.SelectedValue = pw.Kategorie.KategorieId;
            TXTPassword.Text = pw.PSW;
            TXTUsername.Text = pw.Login;
        }

        public UC_Password(MainWindow parent, DAL.Passwort passwort)
        {
            this.parent = parent;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void BTNSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DATECreationDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DATEExpirationDate.SelectedDate != null)
            {
                if (DATEExpirationDate.SelectedDate.Value.Date.AddDays(1) < DATECreationDate.SelectedDate.Value.Date)
                {
                    DATEExpirationDate.SelectedDate = DATECreationDate.SelectedDate.Value.Date.AddDays(1);
                }

                DATEExpirationDate.DisplayDateStart = DATECreationDate.SelectedDate.Value.Date.AddDays(1);
            }
        }
    }

    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueToConvert = Double.Parse(value.ToString());
            var factor = Int32.Parse(parameter.ToString());
            return valueToConvert * (factor/10);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
