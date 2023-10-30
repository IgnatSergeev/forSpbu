namespace SimpleFtp.Protocol;

public class GetResponse : Response
{
    public byte[] File { get; private set; }
    
    public GetResponse(byte[] fileBytes)
    {
        File = fileBytes;
    }
    
    public GetResponse()
    {
    }
}