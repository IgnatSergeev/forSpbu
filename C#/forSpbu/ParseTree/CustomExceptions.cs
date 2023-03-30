namespace ParseTree;

public class ParseErrorException : Exception
{
    public ParseErrorException(string message) : base(message) {}
    public ParseErrorException() {}
}