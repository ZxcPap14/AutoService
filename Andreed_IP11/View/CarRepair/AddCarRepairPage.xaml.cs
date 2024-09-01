using Andreed_IP11.Model;
using Andreed_IP11.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Andreed_IP11.View.CarRepair
{
    /// <summary>
    /// Логика взаимодействия для AddCarRepairPage.xaml
    /// </summary>
    public partial class AddCarRepairPage : Page
    {
        int selectedId;
        Core db = new Core();
        public AddCarRepairPage()
        {
            InitializeComponent();
            LoadWorker();
        }
        public class PartInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private void LoadWorker()
        {
            List<PartInfo> partsList = db.context.Employees
               .Select(p => new PartInfo { Id = p.EmployeeID, Name = p.Name })
               .ToList();
            EmployeeID.ItemsSource = partsList;
            EmployeeID.DisplayMemberPath = "Name";
            EmployeeID.SelectedValuePath = "Id";
        }
        private void AddRepairButton_Click(object sender, RoutedEventArgs e)
        {
            var zxc = new Repairs
            {
                Vehicle = CarNumberTextBox.Text,
                EmployeeID = selectedId,
                RepairDate = EndDatePicker.SelectedDate ?? DateTime.MinValue,
                Status = StatusComboBox.Text,
                EstimatedCost = Convert.ToDecimal(PreliminaryEstimateTextBox.Text),
                FinalCost = Convert.ToDecimal(FinalCostTextBox.Text),
                Description = DescriptionTextBox.Text,
            };
            db.context.Repairs.Add(zxc);
            db.context.SaveChanges();
            MessageBox.Show("Успех");
            int lastId = db.context.Users
                        .OrderByDescending(u => u.UserID)
                        .Select(u => u.UserID)
                        .FirstOrDefault();
            UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} добавил данные о ремонте - {lastId}");

            this.NavigationService.Navigate(new CarRepair.CarRepairManagementPage());
        }


        private void EmployeeID_Selected(object sender, SelectionChangedEventArgs e)
        {
            selectedId = (int)EmployeeID.SelectedValue;
            //MessageBox.Show(selectedId.ToString());
        }
    }
}
