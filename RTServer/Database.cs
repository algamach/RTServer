using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTServer
{
    internal class Database
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-TR2CJF1\\SQLEXPRESS;Initial Catalog=RTdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection getConnection()
        {
            return connection;
        }

        public DataTable getSqlQuery(string sqlString)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            SqlCommand command = new SqlCommand(sqlString, getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
        public bool insertSqlCommand(string queryString)
        {
            bool result;
            SqlCommand insert_command = new SqlCommand(queryString, getConnection());

            openConnection();
            if (insert_command.ExecuteNonQuery() == 1)
            {                
                result = true;
            }
            else
            {
                result = false; 
            }
            closeConnection();
            return result;
        }
    }
}
