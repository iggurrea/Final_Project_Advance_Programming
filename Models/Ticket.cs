using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Ticket
    {
        public int Id { get; set; } 
        public string Username { get; set; } 
        public string TicketType { get; set; } // 'Hardware', 'Software'
        public DateTime CreatedAt { get; set; } 
        public string Status { get; set; } // 'Unanswered', 'In Service', 'Fulfilled'
        public string ServiceStatus { get; set; } // 'Open', 'Resolved', 'Not Resolved'
        public abstract string GetSummary(); 
    }

    public class HardwareTicket : Ticket
    {
        public string Equipment { get; set; }
        public string Malfunction { get; set; }

        public override string GetSummary()
        {
            return $"[HW] {Equipment} - {Malfunction}";
        }
    }

    public class SoftwareTicket : Ticket
    {
        public string Software { get; set; }
        public string Description { get; set; }

        public override string GetSummary()
        {
            return $"[SW] {Software} - {Description}";
        }
    }
}
