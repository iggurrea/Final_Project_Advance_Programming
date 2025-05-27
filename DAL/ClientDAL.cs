using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    public class ClientDAL
    {
        private static string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactsBook;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        //Insert, Update, Delete
        public static bool execINSERT(Client c)
        {
            string sql = $"insert into Contacts values('{c.ID}','{c.Name}','{c.TypeService}','{c.Agenda}');";
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Select
        public static DataTable? GetClients()
        {
            string sql = "select * from Client;";
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }           
}
