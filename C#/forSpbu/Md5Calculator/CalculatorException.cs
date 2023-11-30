namespace Md5Calculator;

public class CalculatorException : ArgumentException
{
    public CalculatorException(string message) : base(message)
    {
    }
    
    public CalculatorException()
    {
    }
}