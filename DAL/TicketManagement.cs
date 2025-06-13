using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    //crear tickets con Factory pattern
    public static class TicketFactory
    {
        public static Ticket CreateTicket(string type)
        {
            return type switch
            {
                "Hardware" => new HardwareTicket { TicketType = "Hardware" },
                "Software" => new SoftwareTicket { TicketType = "Software" },
                _ => throw new ArgumentException("Tipo inválido de ticket"),
            };
        }
    }
    public class TicketManagement
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["Ticket2HelpConnection"].ConnectionString;

        public List<Ticket> GetTicketsByUser(string username)
        {
            var tickets = new List<Ticket>();
            string query = "SELECT * FROM Tickets WHERE Username = @username";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string type = reader["TicketType"].ToString();

                    try
                    {
                        Ticket ticket = TicketFactory.CreateTicket(type);

                        // Propiedades comunes
                        ticket.Id = Convert.ToInt32(reader["Id"]);
                        ticket.Username = reader["Username"].ToString();
                        ticket.TicketType = type;
                        ticket.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                        ticket.Status = reader["Status"].ToString();
                        ticket.ServiceStatus = reader["ServiceStatus"].ToString();

                        // Propiedades específicas
                        if (ticket is HardwareTicket hw)
                        {
                            hw.Equipment = reader["Equipment"]?.ToString();
                            hw.Malfunction = reader["Malfunction"]?.ToString();
                        }
                        else if (ticket is SoftwareTicket sw)
                        {
                            sw.Software = reader["Software"]?.ToString();
                            sw.Description = reader["Description"]?.ToString();
                        }

                        tickets.Add(ticket);
                    }
                    catch (ArgumentException)
                    {
                        // Si el tipo es inválido, lo ignoramos
                        continue;
                    }
                }
            }
            return tickets;
        }


        public bool InsertTicket(Ticket ticket)
        {
            string query = @"INSERT INTO Tickets 
            (Username, TicketType, CreatedAt, Status, ServiceStatus, Description, Equipment, Malfunction, Software) 
            VALUES 
            (@username, @ticketType, @createdAt, @status, @serviceStatus, @description, @equipment, @malfunction, @software)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", ticket.Username);
                cmd.Parameters.AddWithValue("@ticketType", ticket.TicketType);
                cmd.Parameters.AddWithValue("@createdAt", ticket.CreatedAt);
                cmd.Parameters.AddWithValue("@status", ticket.Status);
                cmd.Parameters.AddWithValue("@serviceStatus", ticket.ServiceStatus ?? "");

                // Inicializar campos específicos
                if (ticket is HardwareTicket hw)
                {
                    cmd.Parameters.AddWithValue("@description", DBNull.Value);
                    cmd.Parameters.AddWithValue("@equipment", hw.Equipment ?? "");
                    cmd.Parameters.AddWithValue("@malfunction", hw.Malfunction ?? "");
                    cmd.Parameters.AddWithValue("@software", DBNull.Value);
                }
                else if (ticket is SoftwareTicket sw)
                {
                    cmd.Parameters.AddWithValue("@description", sw.Description ?? "");
                    cmd.Parameters.AddWithValue("@equipment", DBNull.Value);
                    cmd.Parameters.AddWithValue("@malfunction", DBNull.Value);
                    cmd.Parameters.AddWithValue("@software", sw.Software ?? "");
                }
                else
                {
                    // fallback
                    cmd.Parameters.AddWithValue("@description", ticket is SoftwareTicket ? ((SoftwareTicket)ticket).Description : "");
                    cmd.Parameters.AddWithValue("@equipment", "");
                    cmd.Parameters.AddWithValue("@malfunction", "");
                    cmd.Parameters.AddWithValue("@software", "");
                }

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }


    

}
