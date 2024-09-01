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
    /// Логика взаимодействия для RedactPartsPage.xaml
    /// </summary>
    public partial class RedactPartsPage : Page
    {
        int selectedId;
        public class PartInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        Core db = new Core();
        public RedactPartsPage()
        {
            InitializeComponent();
            LoadNames();
        }
        private void LoadNames()
        {

            List<PartInfo> partsList = db.context.Parts
                .Select(p => new PartInfo { Id = p.PartID, Name = p.Name })
                .ToList();
            comboBoxNames.ItemsSource = partsList;
            comboBoxNames.DisplayMemberPath = "Name";
            comboBoxNames.SelectedValuePath = "Id";

        }

        private void comboBoNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedId = (int)comboBoxNames.SelectedValue;
            var partZ = db.context.Parts.FirstOrDefault(s => s.PartID == selectedId);
            NameTextBox.Text = partZ.Name;
            PriceTextBox.Text = ((int)partZ.Price).ToString();
            QuantityTextBox.Text = partZ.Stock.ToString();
            SupplierTextBox.Text = partZ.Supplier;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPart = db.context.Parts.FirstOrDefault(u => u.PartID == selectedId);
            selectedPart.Name = NameTextBox.Text;
            selectedPart.Price = Convert.ToDecimal(PriceTextBox.Text);
            selectedPart.Stock = Convert.ToInt32(QuantityTextBox.Text);
            selectedPart.Supplier = SupplierTextBox.Text;
            UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} обновил данные о детале - {selectedPart.Name}");
            db.context.SaveChanges();
            this.NavigationService.Navigate(new Zapchasti.PartsManagementPage());

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
