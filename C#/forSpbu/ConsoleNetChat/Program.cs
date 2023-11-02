using System.Net;
using ConsoleNetChat;

switch (args.Length)
{
    case < 1 or > 2:
        AsyncConsole.WriteLine("Incorrect arguments");
        return;
    case 1:
    {
        if (!int.TryParse(args[0], out var port))
        {
            AsyncConsole.WriteLine("Incorrect port");
            return;
        }
        
        var server = new Server();
        try
        {
            await server.Connect(port);
        }
        catch (ChatterException e)
        {
            AsyncConsole.WriteLine(e.Message);
            return;
        }
        
        try
        {
#pragma warning disable CS4014 // This is started in background thread
            server.Listen();
#pragma warning restore CS4014 // This is started in background thread
        }
        catch (ChatterException e)
        {
            AsyncConsole.WriteLine(e.Message);
            return;
        }
        
        while (true)
        {
            var message = Console.ReadLine();
            if (string.IsNullOrEmpty(message))
            {
                continue;
            }
            await server.SendMessage(message);
            if (message == "exit")
            {
                server.Disconnect();
                return;
            }
        }

        break;
    }
    default:
    {
        if (!int.TryParse(args[1], out var port))
        {
            AsyncConsole.WriteLine("Incorrect port");
            return;
        }
        if (!IPAddress.TryParse(args[0], out var addr))
        {
            AsyncConsole.WriteLine("Incorrect address");
            return;
        }

        var client = new Client();
        try
        {
            await client.Connect(addr, port);
        }
        catch (ChatterException e)
        {
            AsyncConsole.WriteLine(e.Message);
            return;
        }
        
        try
        {
#pragma warning disable CS4014 // This is started in background thread
            client.Listen();
#pragma warning restore CS4014 // This is started in background thread
        }
        catch (ChatterException e)
        {
            AsyncConsole.WriteLine(e.Message);
            return;
        }

        while (true)
        {
            var message = Console.ReadLine();
            if (string.IsNullOrEmpty(message))
            {
                continue;
            }
            await client.SendMessage(message);
            if (message == "exit")
            {
                client.Disconnect();
                return;
            }
        }

        break;
    }
}