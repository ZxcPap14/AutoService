using Andreed_IP11.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Andreed_IP11.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            LoadMenuForRole(UserVM.Rolesss);
        }
        private void LoadMenuForRole(string role)
        {
            switch (role)
            {
                case "Админ":
                    ShowAdminMenu();
                    break;
                case "Руководитель":
                    ShowManagerMenu();
                    break;
                case "Работник":
                    ShowEmployeeMenu();
                    break;
            }
        }

        private void ShowAdminMenu()
        {
            MainMenu.Items.Clear();
            MainMenu.Items.Add(CreateMenuItem("Учет запчастей", PartsManagement_Click));
            MainMenu.Items.Add(CreateMenuItem("Учет заказов", OrdersManagement_Click));
            MainMenu.Items.Add(CreateMenuItem("Учет ремонтов", RepairsManagement_Click));
            MainMenu.Items.Add(CreateMenuItem("Статистика", Statistics_Click));
            MainMenu.Items.Add(CreateMenuItem("Управление пользователями", UserManagement_Click));
            MainMenu.Items.Add(CreateMenuItem("Управление цехами и сотрудниками", DepartmentsAndEmployees_Click));
            MainMenu.Items.Add(CreateMenuItem("Журнал событий", EventsLog_Click));
        }

        private void ShowManagerMenu()
        {
            MainMenu.Items.Clear();
            MainMenu.Items.Add(CreateMenuItem("Учет запчастей", PartsManagement_Click));
            MainMenu.Items.Add(CreateMenuItem("Учет заказов", OrdersManagement_Click));
            MainMenu.Items.Add(CreateMenuItem("Учет ремонтов", RepairsManagement_Click));
            MainMenu.Items.Add(CreateMenuItem("Статистика", Statistics_Click));
            MainMenu.Items.Add(CreateMenuItem("Управление цехами и сотрудниками", DepartmentsAndEmployees_Click));
        }

        private void ShowEmployeeMenu()
        {
            MainMenu.Items.Clear();
            MainMenu.Items.Add(CreateMenuItem("Учет запчастей", PartsManagement_Click));
            MainMenu.Items.Add(CreateMenuItem("Учет заказов", OrdersManagement_Click));
            MainMenu.Items.Add(CreateMenuItem("Учет ремонтов", RepairsManagement_Click));
        }

        private MenuItem CreateMenuItem(string header, RoutedEventHandler clickHandler)
        {
            MenuItem menuItem = new MenuItem
            {
                Header = header
            };
            menuItem.Click += clickHandler;
            return menuItem;
        }

        // Обработчики кликов
        private void PartsManagement_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Zapchasti.PartsManagementPage();
        }

        private void OrdersManagement_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Zakazi.ZakaziManagementPage();
        }

        private void RepairsManagement_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new CarRepair.CarRepairManagementPage();
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Diogram.DiogramPage1();
        }

        private void UserManagement_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new UserManagement.UserManagementPage();
        }

        private void DepartmentsAndEmployees_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Department.DepartmentManagementPage();
        }

        private void EventsLog_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new EventLog.EventLogPage();
        }

    }
}
