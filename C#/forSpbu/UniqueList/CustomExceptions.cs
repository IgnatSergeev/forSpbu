namespace UniqueList;

public class ValueDoesntExistException : Exception
{
    public ValueDoesntExistException(string message) : base(message) {}
}
public class ValueAlreadyExistException : Exception
{
    public ValueAlreadyExistException(string message) : base(message) {}
}