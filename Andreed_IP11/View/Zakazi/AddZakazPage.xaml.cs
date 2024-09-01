using Andreed_IP11.Model;
using Andreed_IP11.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Andreed_IP11.View.Zapchasti.RedactPartsPage;

namespace Andreed_IP11.View.Zakazi
{
    /// <summary>
    /// Логика взаимодействия для AddZakazPage.xaml
    /// </summary>
    public partial class AddZakazPage : Page
    {
        int selectedId = -1;
        Core db = new Core();
        public AddZakazPage()
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
        private void AddOrderDetailButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show((OrderDatePicker.SelectedDate ?? DateTime.MinValue).ToString());
            var neworder = new Orders
            {
                CustomerName = CustomerNameTextBox.Text,
                OrderDate = OrderDatePicker.SelectedDate ?? DateTime.MinValue,
                Status = (QuantityTextBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                TotalAmount = Convert.ToDecimal(PriceTextBox.Text),
            };
            var neworderdetails = new OrderDetails
            {
                OrderID = neworder.OrderID,
                PartID = selectedId,
                Quantity = Convert.ToInt32(QuantityTextBox22.Text),
                Price = Convert.ToDecimal(PriceTextBox.Text),
            };
            db.context.Orders.Add(neworder);
            db.context.OrderDetails.Add(neworderdetails);
            db.context.SaveChanges();
            MessageBox.Show("Успех");
            UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} добавил заказ номер - {neworder.OrderID} ({neworder.CustomerName})");
            this.NavigationService.Navigate(new Zakazi.ZakaziManagementPage());
        }

        private void comboBoNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedId = (int)comboBoxNames.SelectedValue;
            maxcount.Text = $"Максимум: {db.context.Parts.FirstOrDefault(u => u.PartID == selectedId).Stock} ";
            QuantityTextBox22.Text = "1";
        }


        private void textox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }

        }

        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selectedId != -1 && QuantityTextBox22.Text != "")
            {

                // MessageBox.Show($"max:{db.context.Parts.FirstOrDefault(u => u.PartID == selectedId).Stock} : now {QuantityTextBox22.Text}");
                if (Convert.ToInt32(QuantityTextBox22.Text) > db.context.Parts.FirstOrDefault(u => u.PartID == selectedId).Stock)
                {
                    QuantityTextBox22.Text = $"{db.context.Parts.FirstOrDefault(u => u.PartID == selectedId).Stock}";
                }
                PriceTextBox.Text = $"{Convert.ToInt32(QuantityTextBox22.Text) * db.context.Parts.FirstOrDefault(u => u.PartID == selectedId).Price}";
            }
            else
            {
                PriceTextBox.Text = "0 ";
            }




        }
    }
}
