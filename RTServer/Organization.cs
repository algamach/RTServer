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
        public string getNameFromId(int id)
        {
            string queryString = $"SELECT [Name] FROM Organizations WHERE Id = '{id}'";
            DataTable table = database.getSqlQuery(queryString);
            string name = table.Rows[0]["Name"].ToString();
            return name;
        }

        internal string getAllNames()
        {
            string queryString = $"SELECT * FROM Organizations";
            DataTable table = database.getSqlQuery(queryString);
            string result = "";
            foreach (DataRow row in table.Rows)
            {
                result +=$"{(string)row["Name"]}+";
            }
            result = result.Remove(result.Length-1);
            return result;
        }

        internal string getUsers(string name)
        {
            string queryString = $"SELECT UserName FROM Users JOIN Organizations ON Users.OrgID = Organizations.Id WHERE Organizations.Name = '{name}'";
            DataTable table = database.getSqlQuery(queryString); 
            string result = "";
            foreach (DataRow row in table.Rows)
            {
                result += $"{(string)row["UserName"]}+";
            }
            result = result.Remove(result.Length - 1);
            return result;
        }
    }
}
