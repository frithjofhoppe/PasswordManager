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
        bool isPasswordVisible = false;
        PasswordBox charedBox;
        TextBox clearBox;
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
            LoadValues(currentPassword);
        }

        public UC_Password(MainWindow parent, DAL.Passwort password, WorkingStatus workingStatus)
        {
            InitializeComponent();
            this.parent = parent;
            this.workingStatus = password == null ? WorkingStatus.NEW : workingStatus;
            this.entityStatus = password == null ? EntityStatus.UNATTACHED : EntityStatus.MODIFIED;
            this.currentPassword = password;
            if (password == null)
            {
                Initialize();
                LoadValues(currentPassword);
            }
            else
            {
                LoadValues(currentPassword);
                Initialize();
            }
        }

        private void Initialize()
        {
            CMBCategory.ItemsSource = BLL.Kategorie.LesenAlle();
            CMBCategory.DisplayMemberPath = "Name";
            CMBCategory.SelectedValuePath = "KategorieId";
            charedBox = TXTPasswordChared;
            clearBox = TXTPasswordClear;

            switch (workingStatus)
            {
                case WorkingStatus.NEW: InitalizeNewStatus(); break;
                case WorkingStatus.LOADED: InitalizeLoadedStatus(); break;
            }

            PNLPasswordField.Children.Clear();
            PNLPasswordField.Children.Add(charedBox);
            TXTPasswordChared.IsEnabled = false;
            clearBox.Text = currentPassword.PSW;
            charedBox.Password = currentPassword.PSW;
        }

        private void InitalizeNewStatus()
        {

            currentPassword = new DAL.Passwort()
            {
                Kategorie = BLL.Kategorie.LesenAlle()[0],
                Eingabedatum = DateTime.Now,
                Ablaufdatum = DateTime.Now.AddDays(1),
                PSW = "",
                Zielsystem = "",
                Login = "",
            };

            DATECreationDate.IsEnabled = false;
        }

        private void InitalizeLoadedStatus()
        {
            CMBCategory.IsEnabled = true;
            DATECreationDate.IsEnabled = false;
            DATEExpirationDate.DisplayDateStart = DATECreationDate.SelectedDate.Value.Date.AddDays(1);
        }

        private void LoadValues(DAL.Passwort pw)
        {
            TXTName.Text = pw.Zielsystem;
            DATECreationDate.SelectedDate = pw.Eingabedatum;
            DATEExpirationDate.SelectedDate = pw.Ablaufdatum;
            CMBCategory.SelectedValue = pw.Kategorie.KategorieId;
            TXTPasswordClear.Text = pw.PSW;
            TXTUsername.Text = pw.Login;
        }

        private void setPasswordVisibility(bool status)
        {
            if (status)
            {
                currentPassword.PSW = charedBox.Password;
                clearBox.Text = currentPassword.PSW;
                PNLPasswordField.Children.Clear();
                PNLPasswordField.Children.Add(clearBox);
            }
            else
            {
                currentPassword.PSW = clearBox.Text;
                charedBox.Password = currentPassword.PSW;
                charedBox.Background = clearBox.Background;
                PNLPasswordField.Children.Clear();
                PNLPasswordField.Children.Add(charedBox);
            }

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
            if (IsSelectionValid())
            {
                if (entityStatus == EntityStatus.MODIFIED)
                {
                    BLL.Passwort.Aktualisieren(currentPassword);
                    MessageBox.Show("The password has been update", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    currentPassword = BLL.Passwort.LesenID(currentPassword.PasswortId);
                }
                if (entityStatus == EntityStatus.UNATTACHED)
                {
                    long id = BLL.Passwort.Erstellen(currentPassword);
                    if (id > 0)
                    {
                        MessageBox.Show("The password has been created", "Created", MessageBoxButton.OK, MessageBoxImage.Information);
                        currentPassword = BLL.Passwort.LesenID(id);
                        LoadValues(currentPassword);
                        entityStatus = EntityStatus.MODIFIED;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please check the data", "Incorrect values", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
            if (IsSelectionValid())
            {
                if (entityStatus == EntityStatus.MODIFIED)
                {
                    MessageBoxResult mbr = MessageBox.Show("Do you realy want to delete the password", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (mbr == MessageBoxResult.Yes)
                    {
                        BLL.Passwort.LoeschenById(currentPassword.PasswortId);
                        MessageBox.Show("Password has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                        parent.LoadView(new UC_Password(parent, Additonal.WorkingStatus.NEW), "New password");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please check the data", "Incorrect values", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
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

        private void BTNChangePasswordMode_Click(object sender, RoutedEventArgs e)
        {
            setPasswordVisibility(!isPasswordVisible);
            isPasswordVisible = !isPasswordVisible;
        }

        private void TXTName_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegexLib.Match(RegexLib.IsNameValid, ((TextBox)sender).Text, ((TextBox)sender));
            currentPassword.Zielsystem = TXTName.Text;
        }

        private void TXTUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegexLib.Match(RegexLib.IsNameValid, ((TextBox)sender).Text, ((TextBox)sender));
            currentPassword.Login = TXTUsername.Text;
        }

        private void TXTPasswordClear_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegexLib.Match(RegexLib.IsPasswordValid, ((TextBox)sender).Text, ((TextBox)sender));
            currentPassword.PSW = TXTPasswordClear.Text;
        }

        private void TXTPasswordChared_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void DATEExpirationDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPassword.Ablaufdatum = DATEExpirationDate.SelectedDate.Value;
        }

        private void CMBCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPassword.Kategorie = (DAL.Kategorie)CMBCategory.SelectedItem;
        }

        private bool IsSelectionValid()
        {
            return
                RegexLib.Match(RegexLib.IsNameValid, TXTName.Text, TXTName) &&
                RegexLib.Match(RegexLib.IsPasswordValid, TXTPasswordClear.Text, TXTPasswordClear);
        }
    }

    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueToConvert = Double.Parse(value.ToString());
            var factor = Int32.Parse(parameter.ToString());
            return valueToConvert * (factor / 10);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
