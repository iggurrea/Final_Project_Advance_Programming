using System;
using System.Configuration;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PA_WPF_DB
{
    /// <summary>
    /// Lógica de interacción para DashboardWindow.xaml
    /// </summary>
    /// 


    public partial class DashboardWindow : Window
    {

        private DashboardStatisticsService statisticsService;

        // Properties bound to the charts
        public SeriesCollection ServiceStatusSeries { get; set; }
        public SeriesCollection AverageTimeSeries { get; set; }
        public string[] TimeLabels { get; set; }
        public string FulfilledPercentageText { get; set; }

        public DashboardWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["Ticket2HelpConnection"].ConnectionString;
            statisticsService = new DashboardStatisticsService(connectionString);

            LoadDashboardData();

            DataContext = this; // Bind data to XAML
        }

        private void LoadDashboardData()
        {
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            // Load percentage of solved/unsolved
            var statusData = statisticsService.GetServiceStatusDistribution();
            ServiceStatusSeries = new SeriesCollection
            {
                new PieSeries { Title = "Solved",  Values = new ChartValues<int> { statusData.Solved }, DataLabels = true },
                new PieSeries { Title = "Unsolved", Values = new ChartValues<int> { statusData.Unsolved }, DataLabels = true }
            };

            // Load average time per type
            var avgTimes = statisticsService.GetAverageServiceTimePerType(startDate, endDate);
            AverageTimeSeries = new SeriesCollection
            {
                new ColumnSeries { Title = "Avg. Time", Values = new ChartValues<double>(avgTimes.Values) }
            };
            TimeLabels = avgTimes.Keys.ToArray();

            // Fulfilled tickets %
            double fulfilled = statisticsService.GetPercentageFulfilledWithinDateRange(startDate, endDate);
            FulfilledPercentageText = $"Fulfilled Tickets: {fulfilled:0.##}%";

            // Refresh bindings
            DataContext = null;
            DataContext = this;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDashboardData();
        }
    }
}
