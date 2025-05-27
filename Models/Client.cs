using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Client
    {
        private static int contID = 0;
        public int? ID { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? TypeService { get; set; } = null;
        public DateTime? Agenda { get; set; } = null;

        public Client() { }

        public Client(string name, string type, DateTime datee)
        {
            this.ID = contID++;
            this.Name = name;
            this.TypeService = type;
            this.Agenda = datee;
        }

        public override string ToString()
        {
            return $"{ID} - {this.Name} - {this.TypeService} - {this.Agenda}"; // Fixed: Corrected 'this.TypeServicePhone' to 'this.TypeService'
        }
    }
}
