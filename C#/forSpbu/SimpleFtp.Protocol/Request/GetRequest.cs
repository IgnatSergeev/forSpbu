namespace SimpleFtp.Protocol;

public class GetRequest : Request
{
    public string Path { get; private set; }
    
    public GetRequest(string path)
    {
        Path = path;
    }
    
    public override string ToString()
    {
        return "2 " + Path + "\n";
    }
}