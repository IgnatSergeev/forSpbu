namespace MatrixMult;

/// <summary>
/// Class for matrix creation exceptions
/// </summary>
public class MatrixCreationException : Exception
{
    public MatrixCreationException() : base()
    {
    }
    
    public MatrixCreationException(string message) : base(message)
    {
    }
}