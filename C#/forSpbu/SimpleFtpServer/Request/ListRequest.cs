namespace SimpleFtp;

public class ListRequest : IRequest
{
    public static string Pattern => "1 (?<path>[1-9a-zA-Z./]+)\n";
    private readonly string _path;
    
    public ListRequest(string path)
    {
        _path = path;
    }
}