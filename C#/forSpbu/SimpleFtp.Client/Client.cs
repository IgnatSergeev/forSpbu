using System.Net.Sockets;
using SimpleFtp.Protocol;

namespace SimpleFtp.Client;

public class FtpClient
{
    private readonly TcpClient _client = new TcpClient();
    private StreamReader? _reader;
    private StreamWriter? _writer;
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
        client._reader = new StreamReader(client._client.GetStream());
        client._writer = new StreamWriter(client._client.GetStream());
        client._writer.AutoFlush = true;
        return client;
    }
    
    public Response SendRequest(Request request)
    {
        _writer?.Write(request.ToString());
        var data = _reader?.ReadLine() + "\n";
        return ResponseFactory.Create(data);
    }

    public void Disconnect()
    {
        _client.Close();
    }
}