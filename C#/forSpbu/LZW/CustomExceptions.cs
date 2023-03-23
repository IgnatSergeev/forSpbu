namespace LZW;

public class UnexpectedBranchingException : Exception
{
    public UnexpectedBranchingException(string message) : base(message) {}
    public UnexpectedBranchingException() {}
}