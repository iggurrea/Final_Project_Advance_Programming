using BLL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PA_WPF_DB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /*
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, rellene todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var userService = new UserService();
            var user = userService.ValidateLogin(username, password);

            if (user != null)
            {
                MessageBox.Show($"Bienvenido, {user.Username} ({user.Role})", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Aquí rediriges según el rol
                if (user.Role == "Technician")
                {
                    var techWindow = new TechnicianDashboard(user);
                    techWindow.Show();
                }
                else
                {
                    var userWindow = new UserDashboard(user);
                    userWindow.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
    }
}