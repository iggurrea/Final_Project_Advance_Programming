using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using Models;
using DAL;

namespace PA_WPF_DB
{
    /// <summary>
    /// Lógica de interacción para TechnicianDashboard.xaml
    /// </summary>
    public partial class TechnicianDashboard : Window
    {
        private readonly TicketManagementBLL _ticketManager = new TicketManagementBLL();

        /// <summary>
        /// technician dashboard constructor that initializes the window and loads all tickets.
        public TechnicianDashboard()
        {
            InitializeComponent();
            LoadAllTickets();
        }

        /// <summary>
        /// function that loads all tickets into the DataGrid.
        /// </summary>

        private void LoadAllTickets()
        {
            List<Ticket> tickets = _ticketManager.GetAllTickets();
            dgAllTickets.ItemsSource = tickets;
        }

        /// <summary>
        /// Function that handles the click event for the Refresh button to reload all tickets.
        /// </summary>
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadAllTickets();
        }

        /// <summary>
        /// Function that handles the click event for the Respond button to open a response window for the selected ticket.
        /// </summary>
        private void Respond_Click(object sender, RoutedEventArgs e)
        {
            Ticket selected = dgAllTickets.SelectedItem as Ticket;
            if (selected == null)
            {
                MessageBox.Show("Select a ticket to respond.");
                return;
            }

            var responseWindow = new TechnicianResponseWindow(selected.Id);
            responseWindow.ShowDialog();
            LoadAllTickets(); // actualizar lista
        }

        /// <summary>
        /// Function that handles the click event for the Close button to close the selected ticket.
        /// </summary>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Ticket selected = dgAllTickets.SelectedItem as Ticket;
            if (selected == null)
            {
                MessageBox.Show("Select a ticket to close.");
                return;
            }

            bool result = _ticketManager.CloseTicket(selected.Id);
            if (result)
                MessageBox.Show("Ticket closed successfully.");
            else
                MessageBox.Show("Failed to close ticket.");

            LoadAllTickets(); // actualizar lista de los tickets disponibles
        }
    }
}

