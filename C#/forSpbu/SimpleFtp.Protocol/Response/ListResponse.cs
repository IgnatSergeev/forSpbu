namespace SimpleFtp.Protocol;

public class ListResponse : Response
{
    public (string name, bool isDir)[] List { get; private set; };
    
    public ListResponse((string name, bool isDir)[] dirList)
    {
        List = dirList;
    }
}