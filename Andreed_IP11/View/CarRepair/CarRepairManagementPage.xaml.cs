using Andreed_IP11.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Andreed_IP11.View.CarRepair
{
    /// <summary>
    /// Логика взаимодействия для CarRepairManagementPage.xaml
    /// </summary>
    public partial class CarRepairManagementPage : Page
    {
        Core db = new Core();
        public CarRepairManagementPage()
        {
            InitializeComponent();
            Update();
        }
        private void Update()
        {
            var zxc = db.context.Repairs.ToList();
            var qwe = zxc.Select(t => new Model.Repairs
            {
                RepairID = t.RepairID,
                Vehicle = t.Vehicle,
                RepairDate = t.RepairDate,
                Status = t.Status,
                EstimatedCost = t.EstimatedCost,
                FinalCost = t.FinalCost,
                Description = t.Description,

            }).ToList();
            RepairsDataGrid.ItemsSource = qwe;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CarRepair.AddCarRepairPage());
        }

        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CarRepair.RedactCarRepairPage());

        }
    }
}
