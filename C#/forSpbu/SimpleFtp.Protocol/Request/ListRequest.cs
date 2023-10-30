namespace SimpleFtp.Protocol;

public class ListRequest : Request
{
    public string Path { get; private set; }
    
    public ListRequest(string path)
    {
        Path = path;
    }
}