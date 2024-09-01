using Andreed_IP11.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;
using System.Windows.Controls;

namespace Andreed_IP11.Diogram
{
    /// <summary>
    /// Логика взаимодействия для DiogramPage1.xaml
    /// </summary>
    public partial class DiogramPage1 : Page
    {
        Core db = new Core();
        public DiogramPage1()
        {
            InitializeComponent();
            LoadEarningsData();
            LoadStockData();
        }
        private void LoadEarningsData()
        {
            // Получаем данные из базы данных, объединяем таблицы Repairs и Employees
            var earningsData = from repair in db.context.Repairs
                               join employee in db.context.Employees
                               on repair.EmployeeID equals employee.EmployeeID
                               group new { repair, employee } by employee.Name into g
                               select new
                               {
                                   EmployeeName = g.Key,
                                   TotalEarnings = g.Sum(x => x.repair.FinalCost)
                               };

            // Преобразуем данные для графика
            var employeeNames = earningsData.Select(e => e.EmployeeName).ToArray();
            var values = earningsData.Select(e => (double)e.TotalEarnings).ToArray();

            // Создаем серию данных
            var series = new ColumnSeries
            {
                Title = "Заработок по сотрудникам",
                Values = new ChartValues<double>(values)
            };

            // Настраиваем график
            EarningsChart.Series = new SeriesCollection { series };
            EarningsChart.AxisX[0].Labels = employeeNames;
        }

        private void LoadStockData()
        {
            // Получаем данные из базы данных
            var stockData = db.context.Parts
                .Select(p => new
                {
                    PartName = p.Name,
                    StockCount = p.Stock
                })
                .ToList();

            // Преобразуем данные для графика
            var partNames = stockData.Select(s => s.PartName).ToArray();
            var values = stockData.Select(s => (double)s.StockCount).ToArray();

            // Создаем серию данных
            var series = new ColumnSeries
            {
                Title = "Количество товара",
                Values = new ChartValues<double>(values)
            };

            // Настраиваем график
            StockChart.Series = new SeriesCollection { series };
            StockChart.AxisX[0].Labels = partNames;
        }
    }
}

