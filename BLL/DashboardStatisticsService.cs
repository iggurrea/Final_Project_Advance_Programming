using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BLL
{
    public class DashboardStatisticsService
    {
        private readonly string connectionString;

        public DashboardStatisticsService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public double GetPercentageFulfilledWithinDateRange(DateTime start, DateTime end)
        {
            string query = @"SELECT 
                                COUNT(*) AS Total,
                                SUM(CASE WHEN Status = 'Fulfilled' THEN 1 ELSE 0 END) AS Fulfilled
                            FROM Tickets 
                            WHERE CreatedAt BETWEEN @start AND @end";

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int total = Convert.ToInt32(reader["Total"]);
                int fulfilled = Convert.ToInt32(reader["Fulfilled"]);
                return total > 0 ? (double)fulfilled / total * 100 : 0;
            }
            return 0;
        }

        public Dictionary<string, int> GetServiceStatusDistribution()
        {
            string query = "SELECT ServiceStatus, COUNT(*) AS Count FROM Tickets GROUP BY ServiceStatus";
            var result = new Dictionary<string, int>();

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string status = reader["ServiceStatus"].ToString();
                int count = Convert.ToInt32(reader["Count"]);
                result[status] = count;
            }
            return result;
        }

        public Dictionary<string, double> GetAverageServiceTimePerType()
        {
            string query = @"SELECT TicketType, 
                                    AVG(DATEDIFF(MINUTE, CreatedAt, AnsweredAt)) AS AvgMinutes
                             FROM Tickets
                             WHERE AnsweredAt IS NOT NULL
                             GROUP BY TicketType";
            var result = new Dictionary<string, double>();

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string type = reader["TicketType"].ToString();
                double avg = Convert.ToDouble(reader["AvgMinutes"]);
                result[type] = avg;
            }
            return result;
        }
    }
}
