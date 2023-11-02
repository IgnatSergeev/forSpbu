namespace ConsoleNetChat;

public class ChatterException : Exception
{
    public ChatterException()
    {
    }
    
    public ChatterException(string message) : base(message)
    {
    }
}