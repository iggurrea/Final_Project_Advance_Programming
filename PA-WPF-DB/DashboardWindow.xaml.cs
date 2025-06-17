using System;
using System.Configuration;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using BLL;
using System.Collections.Generic;
using System.Linq;

namespace PA_WPF_DB
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
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

            string connectionString = ConfigurationManager
                .ConnectionStrings["Ticket2HelpConnection"].ConnectionString;

            statisticsService = new DashboardStatisticsService(connectionString);

            LoadDashboardData();
            DataContext = this; // Bind data to XAML
        }

        private void LoadDashboardData()
        {
            // Convert nullable DatePickers to non-nullable with default values
            DateTime start = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime end = EndDatePicker.SelectedDate ?? DateTime.MaxValue;

            // Get fulfilled ticket percentage
            double fulfilled = statisticsService.GetPercentageFulfilledWithinDateRange(start, end);
            FulfilledPercentageText = $"Fulfilled Tickets: {fulfilled:0.##}%";

            // Get status distribution
            var statusData = statisticsService.GetServiceStatusDistribution(start, end);
            ServiceStatusSeries = new SeriesCollection();

            // Add series based on available keys
            if (statusData.ContainsKey("Solved"))
            {
                ServiceStatusSeries.Add(new PieSeries
                {
                    Title = "Solved",
                    Values = new ChartValues<int> { statusData["Solved"] },
                    DataLabels = true
                });
            }
            if (statusData.ContainsKey("Unsolved"))
            {
                ServiceStatusSeries.Add(new PieSeries
                {
                    Title = "Unsolved",
                    Values = new ChartValues<int> { statusData["Unsolved"] },
                    DataLabels = true
                });
            }

            // Get average service time
            var avgTimes = statisticsService.GetAverageServiceTimePerType(start, end);
            AverageTimeSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Avg. Time",
                    Values = new ChartValues<double>(avgTimes.Values)
                }
            };
            TimeLabels = avgTimes.Keys.ToArray();

            // Refresh data bindings
            DataContext = null;
            DataContext = this;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDashboardData();
        }

        /// <summary>
        /// Handles the back button click to return to the technician dashboard.
        /// </summary>
        private void BackToTickets_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Simply closes the dashboard window
        }
    }
}

