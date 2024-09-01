using System.Windows;

namespace Andreed_IP11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new View.Auth.AuthPage());
            //MainFrame.Navigate(new View.MainPage());
            // MainFrame.Navigate(new View.Zapchasti.RedactPartsPage());
        }
    }
}
