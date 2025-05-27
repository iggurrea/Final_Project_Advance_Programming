using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ClientBLL
    {
        public static string Insert(string name, string type, DateTime dateTimee)
        {
            Client client = new Client(name, type, dateTimee);
            if (ClientDAL.execINSERT(client))
                return $"Success: {client.ToString()}";
            else
                return "Error";
        }

        public static DataTable? GetClients()
        {
            DataTable? dt = ClientDAL.GetClients();
            return dt;
        }
    }
}
