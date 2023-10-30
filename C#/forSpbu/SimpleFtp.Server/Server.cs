using System.Diagnostics;
using System.Net;
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
            await HandleClient(client.GetStream());
        }
    }

    private static async Task HandleClient(Stream stream)
    {
        using var reader = new StreamReader(stream);
        await using var writer = new StreamWriter(stream);
        writer.AutoFlush = true;
        try
        {
            var response = HandleRequest(RequestFactory.Create(await reader.ReadToEndAsync()));
            await writer.WriteAsync(response.ToString());
        }
        catch (Exception e) when (e is ArgumentOutOfRangeException or ObjectDisposedException or InvalidOperationException or RequestParseException)
        {
        }
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