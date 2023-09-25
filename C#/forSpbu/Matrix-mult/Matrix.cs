namespace Matrix_mult;

public class Matrix
{
    /// <summary>
    /// Default matrix constructor, creates matrix with one element
    /// </summary>
    public Matrix()
    {
        this.elements = new int[this.Height, this.Width];
    }

    /// <summary>
    /// Matrix constructor with given elements
    /// </summary>
    /// <param name="newElements">Matrix elements</param>
    public Matrix(int [,] newElements, int height, int width)
    {
        Height = height;
        Width = width;
        this.elements = newElements;
    }
    
    /// <summary>
    /// Parses matrix with give string lines
    /// </summary>
    /// <param name="lines">Array of lines to parse matrix from</param>
    /// <returns>Bool determined by parse success</returns>
    public bool Parse(string[] lines)
    {
        var splitLines = lines.Select(str => str.Split(' ')).ToArray();
        var localHeight = splitLines.Length;
        var localWidth = splitLines[0].Length;

        this.elements = new int[localHeight, localWidth];
        for (int i = 0; i < localHeight; i++)
        {
            if (splitLines[i].Length != localWidth)
            {
                return false;
            }

            for (var j = 0; j < localWidth; j++)
            {
                bool parseResult = int.TryParse(splitLines[i][j], out var parseValue);
                if (!parseResult)
                {
                    return false;
                }
                this.elements[i, j] = parseValue;
            }
        }

        this.Height = localHeight;
        this.Width = localWidth;

        return true;
    }

    /// <summary>
    /// Get element by index
    /// </summary>
    /// <param name="firstInd">First index</param>
    /// <param name="secondInd">Second index</param>
    /// <returns>Value of matrix element in given position</returns>
    /// <exception cref="IndexOutOfRangeException">If some index is out of range</exception>
    public int GetElement(int firstInd, int secondInd) =>
        firstInd >= 0 && firstInd < this.Height && secondInd >= 0 && secondInd < this.Width
            ? elements[firstInd, secondInd]
            : throw new ArgumentOutOfRangeException();

    /// <summary>
    /// Number of matrix rows
    /// </summary>
    public int Height{ get; private set; }
    
    /// <summary>
    /// Number of matrix columns
    /// </summary>
    public int Width{ get; private set; }

    public void Print()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Console.Write($"{elements[i, j]} ");
            }
            Console.WriteLine();
        }
    }
    
    private int[,] elements;
}