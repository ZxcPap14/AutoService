using Andreed_IP11.Model;
using Andreed_IP11.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Andreed_IP11.View.Zakazi
{
    /// <summary>
    /// Логика взаимодействия для RedactZakazPage.xaml
    /// </summary>
    public partial class RedactZakazPage : Page
    {
        int selectedId;
        int sselectedId;
        Core db = new Core();
        public RedactZakazPage()
        {
            InitializeComponent();
            LoadNames();
            LoadNamesParts();
        }
        public class PartInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private void comboBoNames_SeletionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedId = (int)comboBoxNames.SelectedValue;
            var zxc = db.context.Orders.FirstOrDefault(u => u.OrderID == selectedId);
            var qwe = db.context.OrderDetails.FirstOrDefault(u => u.OrderID == selectedId);
            CustomerNameTextBox.Text = zxc.CustomerName;
            OrderDatePicker.SelectedDate = zxc.OrderDate;
            QuantityTextBox.Text = zxc.Status;
            zxcomboBoxNames.Text = (db.context.Parts.FirstOrDefault(u => u.PartID == qwe.PartID).Name).ToString();
            QuantityTextBox22.Text = qwe.Quantity.ToString();
            PriceTextBox.Text = qwe.Price.ToString();
        }
        private void LoadNames()
        {

            List<PartInfo> partsList = db.context.Orders
                .Select(p => new PartInfo { Id = p.OrderID, Name = p.CustomerName })
                .ToList();
            comboBoxNames.ItemsSource = partsList;
            comboBoxNames.DisplayMemberPath = "Name";
            comboBoxNames.SelectedValuePath = "Id";



        }

        private void LoadNamesParts()
        {

            List<PartInfo> partsList = db.context.Parts
                .Select(p => new PartInfo { Id = p.PartID, Name = p.Name })
                .ToList();
            zxcomboBoxNames.ItemsSource = partsList;
            zxcomboBoxNames.DisplayMemberPath = "Name";
            zxcomboBoxNames.SelectedValuePath = "Id";

        }
        private void ccomboBoNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sselectedId = (int)zxcomboBoxNames.SelectedValue;
            maxcount.Text = $"Максимум: {db.context.Parts.FirstOrDefault(u => u.PartID == sselectedId).Stock} ";
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
                if (Convert.ToInt32(QuantityTextBox22.Text) > db.context.Parts.FirstOrDefault(u => u.PartID == sselectedId).Stock)
                {
                    QuantityTextBox22.Text = $"{db.context.Parts.FirstOrDefault(u => u.PartID == sselectedId).Stock}";
                }
                PriceTextBox.Text = $"{Convert.ToInt32(QuantityTextBox22.Text) * db.context.Parts.FirstOrDefault(u => u.PartID == sselectedId).Price}";
            }
            else
            {
                PriceTextBox.Text = "0 ";
            }
        }

        private void AddOrderDetailButton_Click(object sender, RoutedEventArgs e)
        {
            var zxc = db.context.Orders.FirstOrDefault(u => u.OrderID == selectedId);
            var qwe = db.context.OrderDetails.FirstOrDefault(u => u.OrderID == selectedId);
            zxc.CustomerName = CustomerNameTextBox.Text;
            zxc.OrderDate = OrderDatePicker.SelectedDate ?? DateTime.MinValue;
            zxc.Status = QuantityTextBox.Text;
            zxc.TotalAmount = Convert.ToDecimal(PriceTextBox.Text);
            qwe.OrderID = selectedId;
            qwe.PartID = sselectedId;
            qwe.Quantity = Convert.ToInt32(QuantityTextBox22.Text);
            qwe.Price = Convert.ToDecimal(PriceTextBox.Text);
            db.context.SaveChanges();
            MessageBox.Show("Успех");
            UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} обновил заказ номер - {zxc.OrderID} ({zxc.CustomerName})");
            this.NavigationService.Navigate(new Zakazi.ZakaziManagementPage());
        }
    }
}
