using SimpleFtp.Client;
using SimpleFtp.Protocol;

var client = await FtpClient.Connect("localhost", 21);
var request = RequestFactory.Create("1 .\n");
var response = client.SendRequest(request);
Console.Write(response.ToString());
client.Disconnect();