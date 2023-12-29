using SimpleFtp;
using SimpleFtp.Protocol;

var server = new FtpServer();
var cancellation = new CancellationTokenSource();
Task.Run(() => server.Listen(cancellation));
var input = Console.ReadLine();
if (input == "exit")
{
    cancellation.Cancel();
}