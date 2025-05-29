using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using Models;

namespace DAL
{
    public class UserManagement
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["Ticket2HelpConnection"].ConnectionString;

        public User? GetUser(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Username, Role FROM Users WHERE Username = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        Username = reader["Username"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
                return null;
            }
        }
    }           
}
