using System.Text.RegularExpressions;
namespace SimpleFtp;

public class Request
{
    public RequestType Type { get; private set; }
    public string Path { get; private set; }
    private readonly string _getPattern = $"({(int)RequestType.Get}) ([1-9a-zA-Z./]+)\n";
    private readonly string _listPattern = $"({(int)RequestType.List}) ([1-9a-zA-Z./]+)\n";

    internal Request(string data)
    {
        if (Regex.IsMatch(data, _getPattern))
        {
            var match = Regex.Match(data, _getPattern);
            Type = RequestType.Get;
            Path = match.Groups[2].Value;
        }
        if (Regex.IsMatch(data, _listPattern))
        {
            var match = Regex.Match(data, _listPattern);
            Type = RequestType.List;
            Path = match.Groups[2].Value;
        }

        throw new RequestParseException();
    }
    
    public enum RequestType
    {
        List = 1,
        Get
    }
}