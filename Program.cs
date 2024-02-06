using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class UDPChatServer
{
    private const int Port = 8888;
    private static UdpClient udpServer;
    private static IPEndPoint clientEndpoint;

    static void Main()
    {
        udpServer = new UdpClient(Port);
        clientEndpoint = new IPEndPoint(IPAddress.Any, 0);

        Console.WriteLine("Сервер запущен. Ожидание сообщений...");

        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();

        while (true)
        {
            string message = Console.ReadLine();
            SendMessage(message);
        }
    }

    private static void ReceiveMessages()
    {
        while (true)
        {
            byte[] data = udpServer.Receive(ref clientEndpoint);
            string receivedMessage = Encoding.UTF8.GetString(data);

            Console.WriteLine($"Клиент ({clientEndpoint.Address}:{clientEndpoint.Port}): {receivedMessage}");
        }
    }

    private static void SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        udpServer.Send(data, data.Length, clientEndpoint);
    }
}