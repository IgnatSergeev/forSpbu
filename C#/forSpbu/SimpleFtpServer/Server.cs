using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace SimpleFtp;
public class FtpServer
{
    private const int Port = 21;
    private readonly TcpListener _listener = new (IPAddress.Any, Port);

    public async Task Listen()
    {
        _listener.Start();
        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();
            Task.Run(() => HandleClient(client.GetStream()));
        }
    }

    private static void HandleClient(NetworkStream stream)
    {
        var reader = new StreamReader(stream);
        var data = reader.ReadToEnd();
        Console.WriteLine($"Received {data}");
    }
}