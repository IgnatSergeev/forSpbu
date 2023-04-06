namespace Routers;

public class WrongGraphException : Exception
{
    public WrongGraphException(string message) : base(message) {}
}
public class ParseException : Exception {}