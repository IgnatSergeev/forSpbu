using System.Text.RegularExpressions;

namespace SimpleFtp.Protocol;

public static partial class RequestFactory
{
    private const string GetPattern = "2 (?<path>[1-9a-zA-Z./]+)\n";
    private const string ListPattern = "1 (?<path>[1-9a-zA-Z./]+)\n";

    public static Request Create(string request)
    {
        if (GetRegex().IsMatch(request))
        {
            var match = GetRegex().Match(request);
            return new GetRequest(match.Groups["path"].Value);
        }
        if (ListRegex().IsMatch(request))
        {
            var match = ListRegex().Match(request);
            return new ListRequest(match.Groups["path"].Value);
        }

        throw new RequestParseException();
    }

    [GeneratedRegex(GetPattern)]
    private static partial Regex GetRegex();
    
    [GeneratedRegex(ListPattern)]
    private static partial Regex ListRegex();
}