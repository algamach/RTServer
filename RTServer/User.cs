using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace RTServer
{
    internal class User
    {
        Database database = new Database();

        private int _id;
        private string _lastName;
        private string _firstName;
        private string _fatherName;
        private int _orgId;
        private string _userName;
        private string _password;

        public int Id { get => _id; set => _id = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string FatherName { get => _fatherName; set => _fatherName = value; }
        public int OrgId { get => _orgId; set => _orgId = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public string logining(string username, string password)
        {
            if (login(username, password))
            {
                UserName = username;
                return ($"login+true+{UserName}");
            }
            else
            {
                return ("login+false");
            }
        }
        public bool login(string username, string password)
        {
            DataTable table = new DataTable();
            string sqlQuery = $"SELECT UserName, [Password] FROM Users WHERE UserName = '{username}' and [Password] = '{password}'";
            table = database.getSqlQuery(sqlQuery);
            if (table.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal string getUserData(string username)
        {
            DataTable table = new DataTable();
            UserName = username;
            string sqlQuery = $"SELECT * FROM Users WHERE UserName = '{UserName}'";
            table = database.getSqlQuery(sqlQuery);

            Id = int.Parse(table.Rows[0]["Id"].ToString());
            LastName = table.Rows[0]["LastName"].ToString();
            FirstName= table.Rows[0]["FirstName"].ToString();
            FatherName = table.Rows[0]["FatherName"].ToString();
            OrgId= int.Parse(table.Rows[0]["OrgID"].ToString());

            Organization org = new Organization();
            string orgName = org.getNameFromId(OrgId);
            string result = $"{UserName}+{LastName}+{FirstName}+{FatherName}+{orgName}";
            return result;
        }
        private Boolean isUserExist(string userName)
        {
            DataTable table = new DataTable();
            string sqlQuery = $"SELECT UserName FROM Users WHERE UserName = '{userName}'";
            table = database.getSqlQuery(sqlQuery);

            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public string signUp(string orgName, string lastName, string firstName, string fatherName, string userName, string password)
        {
            if ((userName == "") || (lastName == "") || (firstName == "") || (orgName == "") || (password == ""))
            {
                return "emptyError";
            }
            else if (isUserExist(userName))
            {
                return "usernameError";
            }
            else 
            {
                Organization org = new Organization();
                int orgId = org.getIdFromName(orgName);
                string queryString = $"INSERT INTO Users (LastName, FirstName, FatherName, UserName, [Password], OrgID) VALUES ('{lastName}', '{firstName}', '{fatherName}', '{userName}', '{password}', {orgId});";

                bool result = database.insertSqlCommand(queryString);
                if (result)
                    return "success";
                else
                    return "error";
            }           
            
        }

    }
}
