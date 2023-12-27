using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using SimpleFtp.Protocol;

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
            using var client = await _listener.AcceptTcpClientAsync();
            await HandleClient(client);
        }
    }

    private static async Task HandleClient(TcpClient client)
    {
        Console.WriteLine("Connected");
        using var reader = new StreamReader(client.GetStream());
        await using var writer = new StreamWriter(client.GetStream());
        writer.AutoFlush = true;
        
        
        while (IsConnected(client))
        {
            try
            {
                var data = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(data))
                {
                    continue;
                }
                data += "\n";
                var response = HandleRequest(RequestFactory.Create(data));
                await writer.WriteAsync(response.ToString());
                Console.Write($"Request: {data}");
                Console.Write($"Response: {response.ToString()}");
            }
            catch (Exception e) when (e is ArgumentOutOfRangeException or ObjectDisposedException or InvalidOperationException or RequestParseException)
            {
            }
        }
        Console.WriteLine("Disconnected");
    }

    private static bool IsConnected(TcpClient client)
    {
        var tcpConnections = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().Where(x => x.LocalEndPoint.Equals(client.Client.LocalEndPoint) && x.RemoteEndPoint.Equals(client.Client.RemoteEndPoint)).ToArray();
        return tcpConnections.Length > 0 && tcpConnections.First().State == TcpState.Established;
    }

    private static Response HandleRequest(Request request)
    {
        switch (request)
        {
            case ListRequest listRequest:
            {
                try
                {
                    var files = Directory.GetFiles(listRequest.Path);
                    var directories = Directory.GetDirectories(listRequest.Path);
                    return new ListResponse(files.Select((string x) => (x, false)).Concat(directories.Select((string x) => (x, true))));
                }
                catch (Exception e) when (e is IOException or UnauthorizedAccessException or ArgumentException)
                {
                }

                return new ListResponse();
            }
            case GetRequest getRequest:
            {
                try
                {
                    return new GetResponse(File.ReadAllBytes(getRequest.Path));
                }
                catch (Exception e) when (e is IOException or UnauthorizedAccessException or ArgumentException or NotSupportedException)
                {
                }

                return new GetResponse();
            }
            default:
            {
                throw new UnreachableException();
            }
        }
    }
}