namespace SimpleFtp.Protocol;

public class ListResponse : Response
{
    private readonly IEnumerable<(string name, bool isDir)>? _list;

    private int Size => _list?.Count() ?? -1;

    public ListResponse(IEnumerable<(string name, bool isDir)> dirList)
    {
        _list = dirList;
    }
    
    public ListResponse()
    {
    }

    public override string ToString() => 
        Size + " " + 
            string.Join(' ', (_list ?? Array.Empty<(string name, bool isDir)>())
                .Select<(string name, bool isDir), string>(x => x.name + " " + x.isDir)) + "\n";
}