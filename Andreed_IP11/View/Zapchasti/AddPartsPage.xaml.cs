using Andreed_IP11.Model;
using Andreed_IP11.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Andreed_IP11.View.Zapchasti
{
    /// <summary>
    /// Логика взаимодействия для AddPartsPage.xaml
    /// </summary>
    public partial class AddPartsPage : Page
    {
        Core db = new Core();
        public AddPartsPage()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание нового объекта запчасти
                var newPart = new Parts
                {
                    Name = NameTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    Stock = int.Parse(QuantityTextBox.Text),
                    Supplier = SupplierTextBox.Text
                };

                // Логика сохранения запчасти в базу данных или другой источник данных
                db.context.Parts.Add(newPart);
                db.context.SaveChanges();

                UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} добавил деталь - {newPart.Name}");


                this.NavigationService.Navigate(new Zapchasti.PartsManagementPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
