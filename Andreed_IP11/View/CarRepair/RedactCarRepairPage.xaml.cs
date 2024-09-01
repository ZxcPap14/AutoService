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
    /// Логика взаимодействия для RedactCarRepairPage.xaml
    /// </summary>
    public partial class RedactCarRepairPage : Page
    {
        int selectedId;
        int selectedIdd;
        Core db = new Core();
        public class PartInfof
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class PartInfoff
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public RedactCarRepairPage()
        {
            InitializeComponent();
            LoadWorker();
            LoadRepair();
        }
        private void LoadWorker()
        {
            List<PartInfof> partsList = db.context.Employees
               .Select(p => new PartInfof { Id = p.EmployeeID, Name = p.Name })
               .ToList();
            EmployeeID.ItemsSource = partsList;
            EmployeeID.DisplayMemberPath = "Name";
            EmployeeID.SelectedValuePath = "Id";
        }
        private void LoadRepair()
        {

            List<PartInfoff> partsList = db.context.Repairs
                .Select(p => new PartInfoff { Id = p.RepairID, Name = p.Vehicle })
                .ToList();
            comboBoxNames.ItemsSource = partsList;
            comboBoxNames.DisplayMemberPath = "Name";
            comboBoxNames.SelectedValuePath = "Id";

        }

        private void comboBoNams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedIdd = (int)comboBoxNames.SelectedIndex + 1;
            //MessageBox.Show(selectedIdd.ToString());
            var zxc = db.context.Repairs.FirstOrDefault(u => u.RepairID == selectedIdd);
            CarNumberTextBox.Text = zxc.Vehicle;
            EndDatePicker.SelectedDate = zxc.RepairDate;
            DescriptionTextBox.Text = zxc.Description;
            EmployeeID.Text = db.context.Employees.FirstOrDefault(u => u.EmployeeID == zxc.EmployeeID).Name;
            StatusComboBox.Text = zxc.Status;
            PreliminaryEstimateTextBox.Text = zxc.EstimatedCost.ToString();
            FinalCostTextBox.Text = zxc.FinalCost.ToString();

        }

        private void EmployeeID_Selected(object sender, SelectionChangedEventArgs e)
        {
            selectedId = (int)EmployeeID.SelectedValue;

        }

        private void RedactRepairButton_Click(object sender, RoutedEventArgs e)
        {
            var zxc = db.context.Repairs.FirstOrDefault(u => u.RepairID == selectedIdd);
            zxc.Vehicle = CarNumberTextBox.Text;
            zxc.EmployeeID = selectedId;
            zxc.RepairDate = EndDatePicker.SelectedDate ?? DateTime.MinValue;
            zxc.Status = StatusComboBox.Text;
            zxc.EstimatedCost = Convert.ToDecimal(PreliminaryEstimateTextBox.Text);
            zxc.FinalCost = Convert.ToDecimal(FinalCostTextBox.Text);
            zxc.Description = zxc.Description;
            db.context.SaveChanges();
            MessageBox.Show("Успех");
            this.NavigationService.Navigate(new CarRepair.CarRepairManagementPage());
            UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} изменил данные о ремонте - {zxc.RepairID}");

        }
    }
}
