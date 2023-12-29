using SimpleFtp.Client;
using SimpleFtp.Protocol;

var client = await FtpClient.Connect("localhost", 32768);
Console.WriteLine("Connected");
while (true)
{
    var command = Console.ReadLine();
    if (command == "exit")
    {
        client.Disconnect();
        break;
    }

    if (command == null)
    {
        Console.WriteLine("Incorrect command");
        continue;
    }
    
    try
    {
        var request = RequestFactory.Create(command + "\n");
        var response = client.SendRequest(request);
        Console.Write(response.ToString());
    }
    catch (RequestParseException)
    {
        Console.WriteLine("Incorrect command");
    }
}
