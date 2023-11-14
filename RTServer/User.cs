using System.ComponentModel.Design;
using System.Data;

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

        public string[] signUp(string lastName, string firstName, string fatherName, string userName, string password, int orgId)
        {
            throw new Exception();
        }


    }
}
