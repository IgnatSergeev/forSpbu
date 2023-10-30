using System.Net;
using System.Net.Sockets;

var client = new TcpClient();
await client.ConnectAsync("localhost", 21);
using var stream = client.GetStream();
using var reader = new StreamReader(stream);
using var writer = new StreamWriter(stream) { AutoFlush = true };

await writer.WriteLineAsync("1 .\n");