using System.Net.Sockets;
using SimpleFtp.Protocol;

namespace SimpleFtp.Client;

public class FtpClient
{
    private readonly TcpClient _client = new TcpClient();
    public string Hostname { get; private set; }
    public int Port { get; private set; }

    private FtpClient(string hostname, int port)
    {
        Hostname = hostname;
        Port = port;
    }
    
    public static async Task<FtpClient> Connect(string hostname, int port)
    {
        var client = new FtpClient(hostname, port);
        await client._client.ConnectAsync(hostname, port);
        return client;
    }
    
    public async Task<Response> SendRequest(Request request)
    {
        using var reader = new StreamReader(_client.GetStream());
        await using var writer = new StreamWriter(_client.GetStream());

        await writer.WriteAsync(request.ToString());
        return ResponseFactory(reader.ReadToEndAsync());
    }
}