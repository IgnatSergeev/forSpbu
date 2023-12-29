using SimpleFtp.Client;
using SimpleFtp.Protocol;

namespace SimpleFtp.Tests;

public class Tests
{
    [Test]
    public void TestListCommand()
    {
        var server = new FtpServer();
        var cancellation = new CancellationTokenSource();
        Task.Run(() => server.Listen(cancellation));

        Directory.CreateDirectory("./tmp");
        File.WriteAllText("./tmp/a.txt", "");
        var request = RequestFactory.Create("1 ./tmp\n");
        var response = FtpClient.Connect("localhost", 32768).Result.SendRequest(request);
        Assert.That(response.ToString(), Is.EqualTo("1 ./tmp\\a.txt False\n"));
    }
    
    [Test]
    public void TestGetCommand()
    {
        var server = new FtpServer();
        var cancellation = new CancellationTokenSource();
        Task.Run(() => server.Listen(cancellation));
        
        var content = "asd@%!@# !@#%&*()";
        File.WriteAllText("./tmp.txt", content);
        var request = RequestFactory.Create("2 ./tmp.txt\n");
        var response = FtpClient.Connect("localhost", 32768).Result.SendRequest(request);
        Assert.That(response.ToString(), Is.EqualTo("17 " + content + "\n"));
    }
    
    [Test]
    public void TestConcurrentCommands()
    {
        var server = new FtpServer();
        var cancellation = new CancellationTokenSource();
        Task.Run(() => server.Listen(cancellation));
        
        var content = "asd@%!@# !@#%&*()";
        File.WriteAllText("./tmp.txt", content);
        var request = RequestFactory.Create("2 ./tmp.txt\n");
        var fstClient = FtpClient.Connect("localhost", 32768).Result;
        var secClient = FtpClient.Connect("localhost", 32768).Result;

        var start = new ManualResetEvent(false);

        var fstThread = new Thread(() =>
        {
            start.WaitOne();
            Assert.That(fstClient.SendRequest(request).ToString(), Is.EqualTo("17 " + content + "\n"));
        });
        var secThread = new Thread(() =>
        {
            start.WaitOne();
            Assert.That(secClient.SendRequest(request).ToString(), Is.EqualTo("17 " + content + "\n"));
        });
        fstThread.Start();
        secThread.Start();
        start.Set();
        fstThread.Join();
        secThread.Join();
    }
}