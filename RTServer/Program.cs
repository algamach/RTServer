using System;
using RTServer;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class AsyncTwoWayServer
{
    static TcpListener server;

    static async Task Main()
    {
        StartServer();
        while (true)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            Console.WriteLine("Клиент подключен.");

            _ = Task.Run(() => HandleClient(client));
        }
        //await Task.Delay(-1); // Бесконечное ожидание, чтобы сервер продолжал работать
    }

    static void StartServer()
    {
        server = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
        server.Start();
        Console.WriteLine("Сервер запущен. Ожидание подключений...");
    }

    static async Task HandleClient(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();

            while (true)
            {
                byte[] receivedBytes = new byte[1024];
                int bytesRead = await stream.ReadAsync(receivedBytes, 0, receivedBytes.Length);
                string receivedData = Encoding.UTF8.GetString(receivedBytes, 0, bytesRead);
                Console.WriteLine("Получено от клиента: " + receivedData);

                try
                {
                    string response = ProcessClientMessage(receivedData);
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseData, 0, responseData.Length);
                    Console.WriteLine("Отправлено: " + response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка "+ ex.Message);

                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка при обработке клиента: " + e.Message);
        }
    }

    static string ProcessClientMessage(string message)
    {
        return doAction(message);
    }



static string doAction(string response)
{
    string[] responseArray = response.ToString().Split('+');
    string action = responseArray[0];

    switch (action)
    {
        case "login":
            User user = new User();
            return user.logining(responseArray[1], responseArray[2]);
        case "signUp":
            User signUpUser = new User();
            return signUpUser.signUp(responseArray[1], responseArray[2], responseArray[3], responseArray[4], responseArray[5], responseArray[6]);
        case "bookSearch":
            Book book = new Book();
            return book.searchResult(responseArray[1]);
        case "getUserData":
            User userdata = new User();
            return userdata.getUserData(responseArray[1]);
        case "getAllOrg":
            Organization org = new Organization();
            return org.getAllNames();
        case "getUsersFromOrg":
            Organization orgUsers = new Organization();
            return orgUsers.getUsers(responseArray[1]);
        
        default:
            return "defautl";
    }
}
}