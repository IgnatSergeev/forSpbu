using System.Text;
using System.Text.RegularExpressions;

namespace SimpleFtp.Protocol;

public static partial class ResponseFactory
{
    private const string ListPattern = "(?<size>[0-9]+) ((?<names>[1-9a-zA-Z./\\\\]+) (?<isDirs>False|True) )*((?<names>[1-9a-zA-Z./\\\\]+) (?<isDirs>False|True))+\n";
    private const string GetPattern = "(?<size>[0-9]+) (?<content>.+)\n";

    public static Response Create(string response)
    {
        if (ListRegex().IsMatch(response))
        {
            var match = ListRegex().Match(response);
            
            var isDirs = match.Groups["isDirs"].Captures.Select(x => x.Value == "True").ToArray();
            var names = match.Groups["names"].Captures.Select(x => x.Value).ToArray();
            if (!int.TryParse(match.Groups["size"].Value, out var size) || names.Length != size)
            {
                throw new ResponseParseException();
            }

            var list = new (string, bool)[size];
            for (int i = 0; i < size; i++)
            {
                list[i] = (names[i], isDirs[i]);
            }
            return new ListResponse(list);
        }
        if (GetRegex().IsMatch(response))
        {
            var match = GetRegex().Match(response);

            var content = match.Groups["content"].Value;
            if (!int.TryParse(match.Groups["size"].Value, out _))
            {
                throw new ResponseParseException();
            }
            
            return new GetResponse(Encoding.ASCII.GetBytes(content));
        }

        throw new ResponseParseException();
    }
    

    [GeneratedRegex(ListPattern)]
    private static partial Regex ListRegex();
    
    [GeneratedRegex(GetPattern)]
    private static partial Regex GetRegex();
}