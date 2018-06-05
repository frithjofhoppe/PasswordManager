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
        string PreName;
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
            PreName = category.Name;
            InitializeComponent();
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
            this.category = EmptyCategory();
            LoadValues(category);
        }

        private DAL.Kategorie EmptyCategory()
        {
            return new DAL.Kategorie()
            {
                Name = ""
            }; 
        }

        private void LoadValues(DAL.Kategorie kategorie)
        {
            TXTName.Text = kategorie.Name;
        }

        private void TXTName_TextChanged(object sender, TextChangedEventArgs e)
        {
            category.Name = TXTName.Text;
            if (entityStatus == EntityStatus.MODIFIED)
            {
                if(category.Name == PreName)
                {
                    TXTName.Background = Brushes.White;
                }
                else
                {
                    RegexLib.Match(RegexLib.IsCategoryValid, TXTName.Text, TXTName);
                }
            } 
        }

        private void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            if(category.Name == PreName)
            {
                MessageBox.Show("No changes", "Unchanged", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (IsSelectionValid())
            {
                if (!BLL.Kategorie.Existiert(TXTName.Text))
                {
                    if (entityStatus == EntityStatus.UNATTACHED)
                    {
                        var id = BLL.Kategorie.Erstellen(category);
                        if (id > 0)
                        {
                            MessageBox.Show("Category has been created", "Created", MessageBoxButton.OK, MessageBoxImage.Information);
                            category = BLL.Kategorie.LesenID(id);
                            parent.UpdateCategoryList();
                            parent.LoadPasswordListView(category);
                            entityStatus = EntityStatus.MODIFIED;
                            workingStatus = WorkingStatus.LOADED;
                        }
                        else
                        {
                            MessageBox.Show("Some error occurred while creating the category", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }else if(entityStatus == EntityStatus.MODIFIED)
                    {
                        BLL.Kategorie.Aktualisieren(category);
                        var entity = BLL.Kategorie.LesenID(category.KategorieId);
                        category = entity;
                        PreName = category.Name;
                        LoadValues(category);
                        parent.UpdateCategoryList();
                        parent.MainTitle.Content = category.Name;
                        MessageBox.Show("The category has been updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("The name of the category is already taken", "Exists", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("The cateory already exists or the content is invalid", "Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsCurrentName()
        {
            return TXTName.Text.Trim().ToLower() == PreName.Trim().ToLower();
        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
            if (entityStatus == EntityStatus.MODIFIED)
            {
                MessageBoxResult mbr = MessageBox.Show("Do you realy want to delete the password", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (mbr == MessageBoxResult.Yes)
                {
                    BLL.Kategorie.LoeschenById(category.KategorieId);
                    MessageBox.Show("Password has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    parent.LoadView(new UC_Password(parent, Additonal.WorkingStatus.NEW), "New password");
                }
            }
            else
            {
                category = EmptyCategory();
                LoadValues(category);
                MessageBox.Show("Content has been cleared", "Cleared", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsCategoryAvailable()
        {
            var entity = BLL.Kategorie.LesenAlle().FirstOrDefault(i => i.Name.Trim().ToLower() == category.Name.Trim().ToLower());
            return entity != null ? true : false;
        }

        private bool IsSelectionValid()
        {
            return RegexLib.Match(RegexLib.IsNameValid, TXTName.Text, TXTName);
        }
    }
}
