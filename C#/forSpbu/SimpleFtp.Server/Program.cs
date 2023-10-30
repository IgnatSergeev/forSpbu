using SimpleFtp;
using SimpleFtp.Protocol;

var server = new FtpServer();
await server.Listen();
