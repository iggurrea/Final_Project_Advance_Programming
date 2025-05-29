using Models;
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
using DAL;

namespace PA_WPF_DB
{
    /// <summary>
    /// Lógica de interacción para UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Window
    {
        public UserDashboard()
        {
            InitializeComponent();
        }

        private readonly string _username;
        private readonly TicketManagement _ticketManager = new TicketManagement();

        public UserDashboard(string username)
        {
            InitializeComponent();
            _username = username;
            LoadTickets();
        }

        //cargar los tickets de cada usuario normal
        private void LoadTickets()
        {
            List<Ticket> tickets = _ticketManager.GetTicketsByUser(_username);
            dgTickets.ItemsSource = tickets;
        }

        //funcionalidad botón update
        private void RefreshTickets_Click(object sender, RoutedEventArgs e)
        {
            LoadTickets();
        }

        //Añadir nuevo ticket
        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            var newTicketWindow = new NewTicketWindow(_username);
            newTicketWindow.ShowDialog();

            // Recargar tickets después de cerrar
            LoadTickets();
        }

    }
}
