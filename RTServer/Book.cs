using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RTServer
{
    internal class Book
    {
        Database database = new Database();

        private int id;
        private string _name;

        public int Id { get => id; set => id = value; }
        public string Name { get => _name; set => _name = value; }

        internal string searchResult(string s)
        {
            DataTable table = new DataTable();
            string sqlQuery = $"SELECT * FROM Books";
            table = database.getSqlQuery(sqlQuery);

            string row = "";
            string result = "";

            for (int i = 0; i < table.Rows.Count; i++)
            {
                row = table.Rows[i]["name"].ToString();
                if (s == "")
                {
                    result += $"* {row}\n";
                }
                else
                if (Regex.IsMatch(row, s, RegexOptions.IgnoreCase))
                {
                    result += $"* {row}\n";
                }
            }
            if (result == "")
                result = "false";
            return result;
        }
    }
}
