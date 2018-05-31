using M120Projekt.Additonal;
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
    /// Interaction logic for UC_Category.xaml
    /// </summary>
    public partial class UC_Category : UserControl
    {
        MainWindow parent;
        DAL.Kategorie category;
        WorkingStatus workingStatus;
        EntityStatus entityStatus;

        public UC_Category()
        {
            InitializeComponent();
        }

        public UC_Category(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.workingStatus = WorkingStatus.NEW;
            this.entityStatus = EntityStatus.UNATTACHED;
            Initalize();
        }

        public UC_Category(MainWindow parent, DAL.Kategorie category, WorkingStatus workingStatus)
        {
            this.parent = parent;
            this.category = category;
            this.workingStatus = category == null ? WorkingStatus.NEW : workingStatus;
            this.entityStatus = category == null ? EntityStatus.UNCHANGED : EntityStatus.MODIFIED;
            Initalize();
        }

        private void Initalize()
        {
            switch (workingStatus)
            {
                case WorkingStatus.NEW: InitializeNewStatus(); break;
                case WorkingStatus.LOADED: InitalizeLoadedStatus(); break;
            }
        }

        private void InitalizeLoadedStatus()
        {
            LoadValues(category); 
        }

        private void InitializeNewStatus()
        {
            this.category = new DAL.Kategorie()
            {
                Name = ""
            };
            LoadValues(category);
        }

        private void LoadValues(DAL.Kategorie kategorie)
        {
            TXTName.Text = kategorie.Name;
        }

        private void TXTName_TextChanged(object sender, TextChangedEventArgs e)
        {
            category.Name = TXTName.Text;
            RegexLib.Match(RegexLib.IsCategoryValid, TXTName.Text, TXTName);
        }

        private void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsSelectionValid())
            {
                if (!IsCategoryAvailable())
                {
                    var id = BLL.Kategorie.Erstellen(category);
                    if(id > 0)
                    {
                        MessageBox.Show("Category has been created", "Created", MessageBoxButton.OK, MessageBoxImage.Information);
                        category = BLL.Kategorie.LesenID(id);
                        LoadValues(category);
                    }
                    else
                    {
                        MessageBox.Show("Some error occurred while creating the category", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The name of the category is already available (make sure your name lowercase && trim is not taken)");
                }
            }
            else
            {
                MessageBox.Show("Please check the content", "Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool IsCategoryAvailable()
        {
            var entity = BLL.Kategorie.LesenAlle().FirstOrDefault(i => i.Name.Trim().ToLower() == category.Name.Trim().ToLower());
            return entity == null ? true : false;
        }

        private bool IsSelectionValid()
        {
            return RegexLib.Match(RegexLib.IsCategoryValid, TXTName.Text, TXTName);
        }
    }
}
