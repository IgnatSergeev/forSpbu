using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ConsoleNetChat;

/// <summary>
/// Represents chat server
/// </summary>
public class Server : Chatter
{
    private TcpListener? _listener;
    
    /// <summary>
    /// Waits for chat client
    /// </summary>
    /// <param name="port">Port to open connection onto</param>
    /// <exception cref="ChatterException">If error occured(e.g. port was occupied)</exception>
    public async Task Connect(int port)
    {
        try
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            Client = await _listener.AcceptTcpClientAsync();
        }
        catch (Exception e) when (e is ArgumentNullException or ArgumentOutOfRangeException or SocketException or InvalidOperationException)
        {
            throw new ChatterException("Connection interrupted");
        }
        Writer = new StreamWriter(Client.GetStream());
        Writer.AutoFlush = true;
        AsyncConsole.WriteLine("Connected");
    }
}