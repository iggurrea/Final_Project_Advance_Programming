using System;
using System.Collections.Generic;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using BLL;

namespace PA_WPF_DB
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        private readonly DashboardStatisticsService statisticsService;

        // Properties for binding to charts
        public SeriesCollection ServiceStatusSeries { get; set; }
        public SeriesCollection AverageTimeSeries { get; set; }
        public List<string> TimeLabels { get; set; }
        public string FulfilledPercentageText { get; set; }

        public DashboardWindow()
        {
            InitializeComponent();

            // TODO: Replace with your actual connection string
            string connectionString = "YourConnectionStringHere";
            statisticsService = new DashboardStatisticsService(connectionString);

            LoadDashboardData();
            DataContext = this;
        }

        /// <summary>
        /// Loads all dashboard data and binds to charts.
        /// </summary>
        private void LoadDashboardData()
        {
            // Get selected dates or use default range
            DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.Now.AddMonths(-1);
            DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.Now;

            // 1. Fulfilled percentage
            double fulfilledPercent = statisticsService.GetPercentageFulfilledWithinDateRange(startDate, endDate);
            FulfilledPercentageText = $"✅ {fulfilledPercent:F2}% of tickets fulfilled between {startDate:d} and {endDate:d}";

            // 2. Pie Chart - Service Status Distribution
            var serviceCounts = statisticsService.GetServiceStatusDistribution();
            ServiceStatusSeries = new SeriesCollection();
            foreach (var kvp in serviceCounts)
            {
                ServiceStatusSeries.Add(new PieSeries
                {
                    Title = kvp.Key,
                    Values = new ChartValues<int> { kvp.Value },
                    DataLabels = true
                });
            }

            // 3. Bar Chart - Average service time per type
            var avgTimes = statisticsService.GetAverageServiceTimePerType();
            AverageTimeSeries = new SeriesCollection();
            TimeLabels = new List<string>();

            var columnSeries = new ColumnSeries
            {
                Title = "Avg Time (min)",
                Values = new ChartValues<double>()
            };

            foreach (var kvp in avgTimes)
            {
                TimeLabels.Add(kvp.Key);
                columnSeries.Values.Add(kvp.Value);
            }

            AverageTimeSeries.Add(columnSeries);

            // Force data binding to refresh
            DataContext = null;
            DataContext = this;
        }

        /// <summary>
        /// Refresh button click event - reloads dashboard stats.
        /// </summary>
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDashboardData();
        }
    }
}

