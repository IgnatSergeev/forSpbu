namespace SimpleFtp.Request;

public class GetRequest : IRequest
{
    public static string Pattern => "2 (?<path>[1-9a-zA-Z./]+)\n";
    private readonly string _path;
    
    public GetRequest(string path)
    {
        _path = path;
    }
}