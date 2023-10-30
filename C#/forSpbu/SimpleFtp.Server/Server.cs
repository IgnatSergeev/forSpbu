using System.Net;
using System.Net.Sockets;

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

    private static async void HandleClient(NetworkStream stream)
    {
        var reader = new StreamReader(stream);
        var data = await reader.ReadToEndAsync();
        var request = Protocol.RequestFactory.Create(data);
        var response = HandleRequest(request);
    }

    private static Protocol.Response HandleRequest(Protocol.Request request)
    {
        if (request is Protocol.GetRequest)
        {
            request.
        }
    }
}