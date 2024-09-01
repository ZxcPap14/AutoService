using Andreed_IP11.Model;
using Andreed_IP11.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Andreed_IP11.View.Auth
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        Core db = new Core();
        public AuthPage()
        {
            InitializeComponent();
            PasswordBox.PasswordChar = '*';

        }

        private bool IsValidCredentials(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string name = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            if (!IsValidCredentials(name, password))
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {

                if (UserVM.CheckAuth(name, password))
                {
                    Properties.Settings.Default.Save();
                    foreach (var user in db.context.Users.ToList().Where(x => x.Username == name && x.PasswordHash == UserVM.HashPassword(password)))
                    {
                        ViewModel.UserVM.Rolesss = user.Role;
                        if (user.Role == "Работник")
                        {
                            this.NavigationService.Navigate(new View.MainPage());
                        }

                        else if (user.Role == "Руководитель")
                        {
                            this.NavigationService.Navigate(new View.MainPage());
                        }

                        else if (user.Role == "Админ")
                        {
                            this.NavigationService.Navigate(new View.MainPage());

                        }

                        else
                        {
                            MessageBox.Show("Ошибка 404");
                        }
                        Properties.Settings.Default.TempUser = user.UserID;
                        Properties.Settings.Default.TempUsername = user.Username;
                        Properties.Settings.Default.Save();
                        UserVM.RegLogs($"Пользователь {Properties.Settings.Default.TempUsername} вошел в приложение");
                    }
                }

                else
                {
                    MessageBox.Show("Неверные данные");
                }

            }
        }
    }
}

