using Andreed_IP11.Model;
using LiveCharts.Defaults;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;

namespace Andreed_IP11.ViewModel
{
    public class UserVM
    {
        public static string Rolesss;
        public static Core bd = new Core();
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static bool CheckAuth(string login, string password)
        {
            if (login != "" && password != "")
            {
                string vrem = HashPassword(password);
                int LegitCheck = bd.context.Users.Where(x => x.Username == login && x.PasswordHash == vrem).Count();
                if (LegitCheck == 0)
                {
                    throw new Exception("Такого пользователя нет");

                }
                else return true;
            }
            else
            {
                throw new Exception("Не все поля заполнены");
            }
        }
        public static void RegLogs(string log)
        {
            EventsLog zxc = new EventsLog
            {
                UserID = Properties.Settings.Default.TempUser,
                EventDate = DateTime.Now,
                EventDescription = log,
            };
            bd.context.EventsLog.Add(zxc);
            bd.context.SaveChanges();
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _execute();
        }
    }

    public class DiogramViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ObservablePoint> _values;
        private ObservablePoint _selectedPoint;
        private Func<double, string> _formatter;
        private double _step;

        public DiogramViewModel()
        {
            Values = new ObservableCollection<ObservablePoint>
        {
            new ObservablePoint(1, 2),
            new ObservablePoint(3, 4),
            new ObservablePoint(5, 6)
        };

            Formatter = value => value.ToString("N");
            Step = 1;

            // Инициализация команды
            AddPointCommand = new RelayCommand(AddDataPoint);
        }

        public ObservableCollection<ObservablePoint> Values
        {
            get => _values;
            set
            {
                _values = value;
                OnPropertyChanged(nameof(Values));
            }
        }

        public ObservablePoint SelectedPoint
        {
            get => _selectedPoint;
            set
            {
                _selectedPoint = value;
                OnPropertyChanged(nameof(SelectedPoint));
            }
        }

        public Func<double, string> Formatter
        {
            get => _formatter;
            set
            {
                _formatter = value;
                OnPropertyChanged(nameof(Formatter));
            }
        }

        public double Step
        {
            get => _step;
            set
            {
                _step = value;
                OnPropertyChanged(nameof(Step));
            }
        }

        public ICommand AddPointCommand { get; }

        private void AddDataPoint()
        {
            // Пример добавления новой точки
            var newPoint = new ObservablePoint(Values.Count + 1, Values.Count * 2);
            Values.Add(newPoint);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
