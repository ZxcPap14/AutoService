using Andreed_IP11.Model;
using Andreed_IP11.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Andreed_IP11.View.Department
{
    /// <summary>
    /// Логика взаимодействия для DepartmentManagementPage.xaml
    /// </summary>
    public partial class DepartmentManagementPage : Page
    {
        Core db = new Core();
        int departID;
        int May_be_Baby;
        public DepartmentManagementPage()
        {
            InitializeComponent();
            UpdateDepart();
            UpdateRabotiaga();
            LoadDepart();
            editDepartmentButton.Visibility = Visibility.Hidden;
            AddDepartmentButton.Visibility = Visibility.Visible;
            prikopdvametra.Visibility = Visibility.Hidden;

        }
        public class PartInffo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public void LoadDepart()
        {
            List<PartInffo> partsList = db.context.Departments
               .Select(p => new PartInffo { Id = p.DepartmentID, Name = p.DepartmentName })
               .ToList();
            zxcPap14.ItemsSource = partsList;
            zxcPap14.DisplayMemberPath = "Name";
            zxcPap14.SelectedValuePath = "Id";

        }
        private void UpdateRabotiaga()
        {
            EmployeesDataGrid.ItemsSource = null;
            var zxc = db.context.Employees.ToList();
            var qwe = zxc.Select(t => new Model.Employees
            {
                EmployeeID = t.EmployeeID,
                Name = t.Name,
                Position = t.Position,
                Department = t.Department,
            }).ToList();
            EmployeesDataGrid.ItemsSource = qwe;
        }
        private void UpdateDepart()
        {
            DepartmentsDataGrid.ItemsSource = null;
            var zxc = db.context.Departments.ToList();
            var qwe = zxc.Select(t => new Model.Departments
            {
                DepartmentID = t.DepartmentID,
                DepartmentName = t.DepartmentName,
            }).ToList();
            DepartmentsDataGrid.ItemsSource = qwe;

        }
        private void RedactButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            if (btn != null)
            {
                departID = (int)btn.Tag;
                var zxc = db.context.Departments.FirstOrDefault(u => u.DepartmentID == departID);
                DepartmentNameTextBox.Text = zxc.DepartmentName;
                editDepartmentButton.Visibility = Visibility.Visible;
                AddDepartmentButton.Visibility = Visibility.Hidden;
            }

        }

        private void YdolButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            if (btn != null)
            {
                // Получение номера протокола из атрибута Tag кнопки
                departID = (int)btn.Tag;
                var zxc = db.context.Departments.FirstOrDefault(u => u.DepartmentID == departID);
                db.context.Departments.Remove(zxc);
                db.context.SaveChanges();
                UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} удалил депортамент - {zxc.DepartmentName} ({zxc.DepartmentID})");
                UpdateDepart();
            }
        }

        private void AddDepartClick(object sender, RoutedEventArgs e)
        {
            if (DepartmentNameTextBox.Text != "")
            {
                Model.Departments zxc = new Departments
                {
                    DepartmentName = DepartmentNameTextBox.Text,
                };
                db.context.Departments.Add(zxc);
                db.context.SaveChanges();
                UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} добавил депортамент - {zxc.DepartmentName} ({zxc.DepartmentID})");

                UpdateDepart();

            }
            else
            {
                MessageBox.Show("Поле ввода пусто");
            }

        }

        private void DepartmentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void editDepartClick(object sender, RoutedEventArgs e)
        {
            if (DepartmentNameTextBox.Text != "")
            {
                var zxc = db.context.Departments.FirstOrDefault(u => u.DepartmentID == departID);
                UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} редактировал депортамент - {zxc.DepartmentName} ({zxc.DepartmentID})");
                zxc.DepartmentName = DepartmentNameTextBox.Text;
                db.context.SaveChanges();
                DepartmentNameTextBox.Text = "";
                editDepartmentButton.Visibility = Visibility.Hidden;
                AddDepartmentButton.Visibility = Visibility.Visible;
                MessageBox.Show("Успех");
                UpdateDepart();
                LoadDepart();
            }
            LoadDepart();
        }



        private void Deportament_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeportButton_Click(object sender, RoutedEventArgs e)
        {
            prikopdvametra.Visibility = Visibility.Visible;
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            if (btn != null)
            {
                May_be_Baby = (int)btn.Tag;
            }
        }

        private void narkota(object sender, RoutedEventArgs e)
        {
            prikopdvametra.Visibility = Visibility.Hidden;
            var zxc = db.context.Employees.FirstOrDefault(u => u.EmployeeID == May_be_Baby);
            zxc.Department = zxcPap14.Text;
            db.context.SaveChanges();
            UpdateRabotiaga();

        }
    }
}
