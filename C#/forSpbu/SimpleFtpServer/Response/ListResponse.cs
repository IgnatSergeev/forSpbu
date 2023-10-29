namespace SimpleFtp.Response;

public class ListResponse : IResponse
{
    private readonly (string name, bool isDir)[] _list;
    
    public ListResponse((string name, bool isDir)[] dirList)
    {
        _list = dirList;
    }
}