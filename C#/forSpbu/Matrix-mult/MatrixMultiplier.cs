namespace Matrix_mult;

public static class MatrixMultiplier
{
    /// <summary>
    /// Performs matrix multiplication in single thread
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

        return new Matrix(newElements, first.Height, second.Width);
    }

    public static Matrix MultiThreaded(Matrix first, Matrix second)
    {
        var threadsAmount = Math.Min(Environment.ProcessorCount, first.Height);

        var threads = new Thread[threadsAmount];
        var newElements = new int[first.Height, second.Width];
        for (int i = 0; i < threadsAmount; i++)
        {
            var rows = new List<int>{};
            for (int row = i; row < first.Height; row += threadsAmount)
            {
                rows.Add(row);
            }

            threads[i] = new Thread(() =>
            {
                foreach (var row in rows)
                {
                    for (int j = 0; j < second.Width; j++)
                    {
                        for (int k = 0; j < second.Height; k++)
                        {
                            newElements[row, j] += first.GetElement(row, k) * second.GetElement(j, k);
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

        return new Matrix(newElements, first.Height, second.Width);
    }
}