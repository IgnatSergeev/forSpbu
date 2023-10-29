using System.Text.RegularExpressions;

namespace SimpleFtp.Request;

public static class Factory
{
    public static IRequest Create(string request)
    {
        if (Regex.IsMatch(request, GetRequest.Pattern))
        {
            var match = Regex.Match(request, GetRequest.Pattern);
            return new GetRequest(match.Groups["path"].Value);
        }
        if (Regex.IsMatch(request, ListRequest.Pattern))
        {
            var match = Regex.Match(request, ListRequest.Pattern);
            return new ListRequest(match.Groups["path"].Value);
        }

        throw new RequestParseException();
    } 
}