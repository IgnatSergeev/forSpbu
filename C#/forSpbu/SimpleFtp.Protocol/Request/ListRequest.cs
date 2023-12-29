namespace SimpleFtp.Protocol;

public class ListRequest : Request
{
    public string Path { get; private set; }
    
    public ListRequest(string path)
    {
        Path = path;
    }

    public override string ToString()
    {
        return "1 " + Path + "\n";
    }
}