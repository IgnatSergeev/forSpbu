namespace MatrixMult;

/// <summary>
/// Class for matrix multiplier exceptions
/// </summary>
public class MatrixMultiplierException : Exception
{
    public MatrixMultiplierException() : base()
    {
    }
    
    public MatrixMultiplierException(string message) : base(message)
    {
    }
}