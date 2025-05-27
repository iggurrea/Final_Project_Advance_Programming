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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            string name = txt_name.Text;
            string phone = txt_phone.Text;

            string msg=ContactBLL.Insert(name, phone);
            MessageBox.Show(msg);
        }

        private void btn_query_Click(object sender, RoutedEventArgs e)
        {
            DataTable? dt = ContactBLL.GetContacts();

            // Bind to DataSet if available
            if (dt != null)
            {
                dgv_contacts.ItemsSource = dt.DefaultView;
            }
            else
            {
                // Handle the case where ds is null (e.g., display a message)
            }
        }

        private void dgv_contacts_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid? grid = sender as DataGrid;
            if (grid != null) {
                var rowview = grid.SelectedItem as DataRowView;
                if (rowview != null)
                {
                    DataRow row = rowview.Row;
                    MessageBox.Show("Row: " + row.ItemArray[0].ToString());
                }
            }
        }
    }
}