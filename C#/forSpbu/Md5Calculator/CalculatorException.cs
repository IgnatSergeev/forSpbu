namespace Md5Calculator;

/// <summary>
/// Represents exception thrown in Md5 calculator
/// </summary>
public class CalculatorException : ArgumentException
{
    /// <summary>
    /// Creates exception with message
    /// </summary>
    /// <param name="message">Exception message</param>
    public CalculatorException(string message) : base(message)
    {
    }
    
    /// <summary>
    /// Creates empty exception
    /// </summary>
    public CalculatorException()
    {
    }
}