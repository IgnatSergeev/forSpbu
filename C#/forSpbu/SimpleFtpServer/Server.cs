using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace SimpleFtp;

public class RequestParseException : ApplicationException
{
    public RequestParseException(string message) : base(message)
    {
    }

    public RequestParseException()
    {
    }
}
public class FtpServer
{
    private const int Port = 21;
    private readonly TcpListener _listener = new (IPAddress.Any, Port);

    public async Task  Listen()
    {
        _listener.Start();
        while (true)
        {
            var socket = await _listener.AcceptSocketAsync();
            Task.Run(async () => HandleClient(socket));
        }
    }

    private void HandleClient(Socket socket)
    {
        var reader = new StreamReader(new NetworkStream(socket));
        var data = reader.ReadLineAsync().Result;
        Console.WriteLine($"Received {data}");
    }
    
    
}