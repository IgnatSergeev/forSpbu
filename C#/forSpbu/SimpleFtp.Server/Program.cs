// See https://aka.ms/new-console-template for more information

using SimpleFtp;

var server = new FtpServer();
await server.Listen();
