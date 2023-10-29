namespace MatrixMult;

/// <summary>
/// Class with matrix container
/// </summary>
public class Matrix
{
    /// <summary>
    /// Constructs empty matrix
    /// </summary>
    public Matrix()
    {
        this._elements = new int[0, 0];
    }
    
    /// <summary>
    /// Constructs matrix with given elements
    /// </summary>
    /// <param name="newElements">Matrix elements</param>
    public Matrix(int [,] newElements)
    {
        this._elements = newElements;
    }
    
    /// <summary>
    /// Constructs matrix with given string lines
    /// </summary>
    /// <param name="lines">Array of lines to build matrix from</param>
    /// <exception cref="MatrixCreationException">If given array of lines is incorrect</exception>
    public Matrix(string[] lines)
    {
        var splitLines = lines.Select(str => str.Split(' ')).ToArray();
        var localHeight = splitLines.Length;
        var localWidth = splitLines[0].Length;

        this._elements = new int[localHeight, localWidth];
        for (int i = 0; i < localHeight; i++)
        {
            if (splitLines[i].Length != localWidth)
            {
                throw new MatrixCreationException("Inconsistent lines length");
            }

            for (var j = 0; j < localWidth; j++)
            {
                if (!int.TryParse(splitLines[i][j], out var parseValue))
                {
                    throw new MatrixCreationException("Incorrect matrix element");
                }
                this._elements[i, j] = parseValue;
            }
        }
    }

    /// <summary>
    /// Returns element by index
    /// </summary>
    /// <param name="firstInd">First index</param>
    /// <param name="secondInd">Second index</param>
    /// <returns>Value of matrix element in given position</returns>
    /// <exception cref="IndexOutOfRangeException">If some index is out of range</exception>
    public int GetElement(int firstInd, int secondInd) =>
        firstInd < 0 || firstInd >= this.Height
            ? throw new ArgumentOutOfRangeException(nameof(firstInd))
            : secondInd < 0 || secondInd >= this.Width 
                ? throw new ArgumentOutOfRangeException(nameof(secondInd))
                : this._elements[firstInd, secondInd];

    /// <summary>
    /// Returns matrix as array of string
    /// </summary>
    /// <returns>String array matrix representation</returns>
    public string[] GetStringRepresentation()
    {
        var lines = new string[this.Height];
        for (int i = 0; i < this.Height; i++)
        {
            for (int j = 0; j < this.Width; j++)
            {
                lines[i] += _elements[i, j] + " ";
            }
        }

        return lines;
    }
    
    /// <summary>
    /// Matrix elements
    /// </summary>
    private readonly int[,] _elements;
    
    /// <summary>
    /// Number of matrix rows
    /// </summary>
    public int Height => this._elements.GetLength(0);

    /// <summary>
    /// Number of matrix columns
    /// </summary>
    public int Width => this._elements.GetLength(1);
}