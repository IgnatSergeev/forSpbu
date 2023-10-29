using System.Net;

FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://127.0.0.1");
request.Method = WebRequestMethods.Ftp.ListDirectory;
using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
{
   response.GetResponseStream();
}