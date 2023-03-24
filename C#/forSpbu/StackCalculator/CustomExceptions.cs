namespace StackCalculator;

public class FieldException : Exception
{
    public FieldException(string message) : base(message) {}
}

public class ParseException : Exception {}