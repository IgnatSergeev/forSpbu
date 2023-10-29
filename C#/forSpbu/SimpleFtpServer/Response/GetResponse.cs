namespace SimpleFtp.Response;

public class GetResponse : IResponse
{
    private readonly byte[] _file;
    
    public GetResponse(byte[] fileBytes)
    {
        _file = fileBytes;
    }
}