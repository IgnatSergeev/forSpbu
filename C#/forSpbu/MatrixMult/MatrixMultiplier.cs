namespace MatrixMult;

/// <summary>
/// Class for matrix multiplication
/// </summary>
public static class MatrixMultiplier
{
    /// <summary>
    /// Performs matrix multiplication in single threaded mode
    /// </summary>
    /// <param name="first">First matrix in multiplication</param>
    /// <param name="second">Second matrix in multiplication</param>
    /// <returns>Result matrix</returns>
    /// <exception cref="MatrixMultiplierException">If matrices are incompatible(width of first and height of second are not equal)</exception>
    public static Matrix SingleThreaded(Matrix first, Matrix second)
    {
        if (first.Width != second.Height)
        {
            throw new MatrixMultiplierException("Incompatible matrices");
        }
        
        var newElements = new int[first.Height, second.Width];
        for (var i = 0; i < first.Height; i++)
        {
            for (var j  = 0; j < second.Width; j++)
            {
                for (var k  = 0; k < second.Height; k++)
                {
                    newElements[i, j] += first.GetElement(i, k) * second.GetElement(k, j);
                }
            }
        }

        return new Matrix(newElements);
    }

    /// <summary>
    /// Performs matrix multiplication in multi threaded mode
    /// </summary>
    /// <param name="first">First matrix in multiplication</param>
    /// <param name="second">Second matrix in multiplication</param>
    /// <returns>Result matrix</returns>
    /// <exception cref="MatrixMultiplierException">If matrices are incompatible(width of first and height of second are not equal)</exception>
    public static Matrix MultiThreaded(Matrix first, Matrix second)
    {
        if (first.Width != second.Height)
        {
            throw new MatrixMultiplierException("Incompatible matrices");
        }
        
        var threadsAmount = Math.Min(Environment.ProcessorCount, first.Height);
        var rowsForThread = first.Height / threadsAmount;
        
        var threads = new Thread[threadsAmount];
        var newElements = new int[first.Height, second.Width];
        for (int i = 0; i < threadsAmount; i++)
        {
            var startRow = i * rowsForThread;
            var endRow = (i == threadsAmount - 1) ? first.Height : (i + 1) * rowsForThread;

            threads[i] = new Thread(() =>
            {
                for (var row = startRow; row < endRow; row++)
                {
                    for (int j = 0; j < second.Width; j++)
                    {
                        for (int k = 0; k < second.Height; k++)
                        {
                            newElements[row, j] += first.GetElement(row, k) * second.GetElement(k, j);
                        }
                    }
                }
            });
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }
        
        foreach (var thread in threads)
        {
            thread.Join();
        }

        return new Matrix(newElements);
    }
}