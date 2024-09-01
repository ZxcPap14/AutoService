using Andreed_IP11.Model;
using Andreed_IP11.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Andreed_IP11.View.Zapchasti
{
    /// <summary>
    /// Логика взаимодействия для PartsManagementPage.xaml
    /// </summary>
    public partial class PartsManagementPage : Page
    {
        int idd = -1;
        Core db = new Core();

        public PartsManagementPage()
        {
            InitializeComponent();
            Update();
        }

        private void Update()
        {
            var zxc = db.context.Parts.ToList();
            var qwe = zxc.Select(t => new Model.Parts
            {
                PartID = t.PartID,
                Name = t.Name,
                Supplier = t.Supplier,
                Stock = t.Stock,
                Price = t.Price,

            }).ToList();
            PartsDataGrid.ItemsSource = qwe;
        }
        private void gridviewUsers1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PartsDataGrid.SelectedItem != null)
            {
                Model.Parts selectedProtocol = (Model.Parts)PartsDataGrid.SelectedItem;
                idd = selectedProtocol.PartID;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            PartsDataGrid.ItemsSource = null;
            var zxc = db.context.Parts.ToList();
            var qwe = zxc.Select(t => new Model.Parts
            {
                PartID = t.PartID,
                Name = t.Name,
                Supplier = t.Supplier,
                Stock = t.Stock,
                Price = t.Price,
            }).ToList();

            int qqw = qwe.Count();
            List<Model.Parts> filteredParts = new List<Model.Parts>();

            for (int i = 0; i < qqw; i++)
            {
                var part = qwe[i];

                if (part.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    filteredParts.Add(part);
                }
            }
            PartsDataGrid.ItemsSource = filteredParts;
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            PartsDataGrid.ItemsSource = null;
            SearchTextBox.Text = "";
            Update();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Zapchasti.AddPartsPage());
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Zapchasti.RedactPartsPage());

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (idd != -1)
            {
                var existing = db.context.Parts.FirstOrDefault(u => u.PartID == idd);
                if (existing != null)
                {
                    db.context.Parts.Remove(existing);
                    db.context.SaveChanges();
                    Update();
                    MessageBox.Show("Успех");
                    UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} удалил деталь - {existing.Name}");

                }

            }
            else
            {
                MessageBox.Show("Не выбрана деталь");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
