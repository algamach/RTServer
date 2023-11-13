using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTServer
{
    internal class Organization
    {
        Database database = new Database();

        private int _id;
        private string _name;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }

        public int getIdFromName(string name)
        {
            string queryString = $"SELECT Id FROM Organizations WHERE [Name] = '{name}'";
            DataTable table = database.getSqlQuery(queryString);
            int id = int.Parse(table.Rows[0]["Id"].ToString());
            return id;
        }
    }
}
