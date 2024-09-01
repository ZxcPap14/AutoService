using Andreed_IP11.Model;
using Andreed_IP11.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Andreed_IP11.View.Zakazi
{
    /// <summary>
    /// Логика взаимодействия для ZakaziManagementPage.xaml
    /// </summary>
    public partial class ZakaziManagementPage : Page
    {
        int idd = -1;
        Core db = new Core();
        public ZakaziManagementPage()
        {
            InitializeComponent();
            Update();
        }
        private void Update()
        {
            var zxc = db.context.Orders.ToList();
            var qwe = zxc.Select(t => new Model.Orders
            {
                OrderID = t.OrderID,
                CustomerName = t.CustomerName,
                OrderDate = t.OrderDate,
                Status = t.Status,

            }).ToList();
            OrdersDataGrid.ItemsSource = qwe;

        }
        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Zakazi.AddZakazPage());
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Zakazi.RedactZakazPage());
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (idd != -1)
            {
                MessageBox.Show($"{idd}");
                var existing = db.context.Orders.FirstOrDefault(u => u.OrderID == idd);
                var existingDop = db.context.OrderDetails.FirstOrDefault(u => u.OrderID == idd);
                if (existing != null)
                {
                    db.context.Orders.Remove(existing);
                    db.context.OrderDetails.Remove(existingDop);
                    db.context.SaveChanges();
                    UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} удалил заказ номер - {existing.OrderID} ({existing.CustomerName})");
                    MessageBox.Show("Успех");
                }
            }
            else
            {
                MessageBox.Show("");
            }
            Update();
        }

        private void gridviewUsrs1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem != null)
            {
                Model.Orders selectedProtocol = (Model.Orders)OrdersDataGrid.SelectedItem;
                idd = selectedProtocol.OrderID;
            }
        }
    }
}
