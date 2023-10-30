namespace SimpleFtp.Protocol;

public class GetResponse : Response
{
    private readonly byte[]? _file;
    private int Size => _file?.Length ?? -1;
    
    public GetResponse(byte[] fileBytes)
    {
        _file = fileBytes;
    }
    
    public GetResponse()
    {
    }
    
    public override string ToString() => Size + " " + System.Text.Encoding.UTF8.GetString(_file ?? Array.Empty<byte>()) + "\n";
}