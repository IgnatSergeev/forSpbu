using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace ConsoleNetChat;

/// <summary>
/// Class for chat member
/// </summary>
public class Chatter
{
    protected TcpClient Client = new ();
    protected StreamWriter? Writer;
    
    /// <summary>
    /// Sends message into chat
    /// </summary>
    /// <param name="message">Message to send</param>
    /// <exception cref="ChatterException">If connection is close</exception>
    public async Task SendMessage(string message)
    {
        if (Writer == null)
        {
            throw new ChatterException("Not started");
        }
        await Writer.WriteLineAsync(message);
    }
    
    private static async Task HandleClient(TcpClient client)
    {
        using var reader = new StreamReader(client.GetStream());
        
        while (IsConnected(client))
        {
            try
            {
                var data = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(data))
                {
                    continue;
                }
                
                AsyncConsole.Write($"External: {data}\n");
            }
            catch (Exception e) when (e is ArgumentOutOfRangeException or ObjectDisposedException or InvalidOperationException)
            {
            }
        }
        
        AsyncConsole.WriteLine("Disconnected");
        Environment.Exit(0);
    }

    private static bool IsConnected(TcpClient client)
    {
        return GetState(client) == TcpState.Established;
    }

    private static TcpState GetState(TcpClient tcpClient)
    {
        try
        {
            var connectionInfo = IPGlobalProperties.GetIPGlobalProperties()
                .GetActiveTcpConnections()
                .SingleOrDefault(x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint));
            return connectionInfo?.State ?? TcpState.Unknown;
        }
        catch (ObjectDisposedException)
        {
            return TcpState.Closed;
        }
    } 
    
    /// <summary>
    /// Finishes the chat
    /// </summary>
    public void Disconnect()
    {
        Client.Close();
        AsyncConsole.WriteLine("Disconnected");
    }
    
    /// <summary>
    /// Listens for external messages
    /// </summary>
    /// <exception cref="ChatterException">If connection is closed</exception>
    public async Task Listen()
    {
        if (!IsConnected(Client))
        {
            throw new ChatterException("Cant listen: no connection");
        }
        await HandleClient(Client);
    }
} 