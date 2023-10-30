namespace SimpleFtp.Protocol;

public class ListResponse : Response
{
    public IEnumerable<(string name, bool isDir)>? List { get; private set; }

    public int Size => List?.Count() ?? -1;

    public ListResponse(IEnumerable<(string name, bool isDir)> dirList)
    {
        List = dirList;
    }
    
    public ListResponse()
    {
    }
}