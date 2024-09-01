using Andreed_IP11.Model;
using Andreed_IP11.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Andreed_IP11.View.UserManagement
{
    /// <summary>
    /// Логика взаимодействия для UserManagementPage.xaml
    /// </summary>
    public partial class UserManagementPage : Page
    {
        string tempstring = "error";

        Core db = new Core();
        public UserManagementPage()
        {
            InitializeComponent();
            HideMenu();
            Update();

        }
        private void Update()
        {
            var zxc = db.context.Users.ToList();
            var qwe = zxc.Select(t => new Model.Users
            {
                UserID = t.UserID,
                Username = t.Username,
                PasswordHash = t.PasswordHash,
                Role = t.Role,

            }).ToList();
            UsersDataGrid.ItemsSource = qwe;

        }
        public void HideMenu()
        {
            menushka.Visibility = Visibility.Hidden;
            DopPrikop.Visibility = Visibility.Hidden;
        }
        int UserIDD;
        private void YdolButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            if (btn != null)
            {
                // Получение номера протокола из атрибута Tag кнопки
                UserIDD = (int)btn.Tag;
                var zxc = db.context.Users.FirstOrDefault(u => u.UserID == UserIDD);
                if (jopa == "Работник")
                {
                    var qwe = db.context.Employees.FirstOrDefault(y => y.UserID == UserIDD);
                    db.context.Employees.Remove(qwe);
                }
                db.context.Users.Remove(zxc);
                db.context.SaveChanges();

                UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} удалил пользователя - {zxc.Username} ({zxc.UserID})");
                Update();


            }
        }
        string starparol = "test";
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            menushka.Visibility = Visibility.Visible;
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            if (btn != null)
            {
                // Получение номера протокола из атрибута Tag кнопки
                UserIDD = (int)btn.Tag;
                var zxc = db.context.Users.FirstOrDefault(u => u.UserID == UserIDD);
                UserIDTextBox.Text = zxc.UserID.ToString();
                UserNameTextBox.Text = zxc.Username;
                UserRoleTextBox.Text = zxc.Role;
                UserPasswordBox.Password = zxc.PasswordHash;
                starparol = zxc.PasswordHash;
                tempstring = $"изменил данные о {zxc.Username} ({zxc.UserID})";
                if (zxc.Role == "Работник")
                {
                    var qwe = db.context.Employees.FirstOrDefault(u => u.UserID == UserIDD);
                    NameWorker.Text = qwe.Name;
                    PosWorker.Text = qwe.Position;
                    DeportWorker.Text = qwe.Department;
                    DopPrikop.Visibility = Visibility.Visible;

                }

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            menushka.Visibility = Visibility.Visible;
            DopPrikop.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            menushka.Visibility = Visibility.Hidden;
            DopPrikop.Visibility = Visibility.Hidden;
            if (starparol == "test")
            {
                Users zxc = new Users
                {

                    Username = UserNameTextBox.Text,
                    Role = UserRoleTextBox.Text,
                    PasswordHash = UserVM.HashPassword(UserPasswordBox.Password)
                };
                db.context.Users.Add(zxc);
                db.context.SaveChanges();
                var qqq = db.context.Users
                   .Where(u => u.Username == UserNameTextBox.Text && u.Role == UserRoleTextBox.Text).FirstOrDefault();
                if (jopa == "Работник")
                {
                    MessageBox.Show(qqq.UserID.ToString());
                    Employees qwe = new Employees
                    {
                        Name = NameWorker.Text,
                        Position = PosWorker.Text,
                        Department = DeportWorker.Text,
                        UserID = qqq.UserID,
                    };
                    db.context.Employees.Add(qwe);

                }
                tempstring = $"добавил нового пользователя - {UserNameTextBox.Text} ({qqq.UserID})";

                db.context.SaveChanges();
            }
            else
            {
                var zxc = db.context.Users.FirstOrDefault(u => u.UserID == UserIDD);
                zxc.Username = UserNameTextBox.Text;
                zxc.Role = UserRoleTextBox.Text;
                if (zxc.PasswordHash != starparol)
                {
                    zxc.PasswordHash = UserPasswordBox.Password;
                }
                if (jopa == "Работник")
                {
                    var qwe = db.context.Employees.FirstOrDefault(y => y.UserID == zxc.UserID);
                    qwe.Name = NameWorker.Text;
                    qwe.Position = PosWorker.Text;
                    qwe.Department = DeportWorker.Text;
                    qwe.UserID = zxc.UserID;
                }
                tempstring = $"изменил данные пользователя - {UserNameTextBox.Text} ({UserIDTextBox.Text})";

                db.context.SaveChanges();
            }
            UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} {tempstring}");


            Update();
        }
        string jopa;
        private void UserRoleTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            // Получаем выбранный элемент
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {

                string selectedContent = selectedItem.Content.ToString();
                jopa = selectedContent;
                if ("Работник" == selectedContent)
                {
                    DopPrikop.Visibility = Visibility.Visible;
                }
                else
                {
                    DopPrikop.Visibility = Visibility.Hidden;
                }
                //MessageBox.Show("Вы выбрали: " + selectedContent);
            }


        }
    }
}
