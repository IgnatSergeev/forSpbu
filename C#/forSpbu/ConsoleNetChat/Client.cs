using System.Net;
using System.Net.Sockets;

namespace ConsoleNetChat;

/// <summary>
/// Represents chat client
/// </summary>
public class Client : Chatter
{
    /// <summary>
    /// Connects to the server
    /// </summary>
    /// <param name="addr">Server address</param>
    /// <param name="port">Server port</param>
    /// <exception cref="ChatterException">If error occured(e.g. no server on that address</exception>
    public async Task Connect(IPAddress addr, int port)
    {
        try
        {
            await Client.ConnectAsync(addr, port);
        }
        catch (Exception e) when (e is ArgumentNullException or ArgumentOutOfRangeException or SocketException or ObjectDisposedException)
        {
            throw new ChatterException("Connection impossible");
        }
        
        Writer = new StreamWriter(Client.GetStream());
        Writer.AutoFlush = true;
        AsyncConsole.WriteLine("Connected");
    }
}