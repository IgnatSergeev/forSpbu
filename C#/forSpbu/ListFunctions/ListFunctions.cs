namespace ListFunctions;

/// <summary>
///  Class with map, filter, fold functions
/// </summary>
public static class ListFunctions
{
    /// <summary>
    /// Transform the sequence element-wise
    /// </summary>
    /// <param name="sequence">Sequence to transform</param>
    /// <param name="transformFunction">Transform function to apply to each element</param>
    /// <typeparam name="T">Sequence element type</typeparam>
    /// <returns>Transformed sequence</returns>
    /// <exception cref="ArgumentNullException">If given sequence is null</exception>
    public static List<T> Map<T>(IEnumerable<T> sequence, Func<T, T> transformFunction)
    {
        if (sequence == null)
        {
            throw new ArgumentNullException(nameof(sequence));
        }

        var newList = new List<T>();
        foreach (var element in sequence)
        {
            newList.Add(transformFunction(element));
        }

        return newList;
    }
    
    /// <summary>
    /// Filter sequence elements into another sequence
    /// </summary>
    /// <param name="sequence">Sequence to filer</param>
    /// <param name="filterFunction">Function, that returns true if element satisfies filter checks</param>
    /// <typeparam name="T">Sequence element type</typeparam>
    /// <returns>Sequence with filtered elements</returns>
    /// <exception cref="ArgumentNullException">If given sequence is null</exception>
    public static List<T> Filter<T>(IEnumerable<T> sequence, Func<T, bool> filterFunction)
    {
        if (sequence == null)
        {
            throw new ArgumentNullException(nameof(sequence));
        }
        
        var newList = new List<T>();
        foreach (var element in sequence)
        {
            if (filterFunction(element))
            {
                newList.Add(element);
            }
        }

        return newList;
    }
    
    /// <summary>
    /// Accumulates value throughout the sequence
    /// </summary>
    /// <param name="sequence">Sequence to accumulate value through</param>
    /// <param name="startValue">Start value of accumulator</param>
    /// <param name="transformFunction">Function to apply accumulation with element</param>
    /// <typeparam name="T">Sequence element type</typeparam>
    /// <typeparam name="U">Accumulator type</typeparam>
    /// <returns>Accumulated value</returns>
    /// <exception cref="ArgumentNullException">If given sequence is null</exception>
    /// <exception cref="ArgumentOutOfRangeException">If given sequence is empty</exception>
    public static U Fold<T, U>(IEnumerable<T> sequence, U startValue, Func<T, U, U> transformFunction)
    {
        if (sequence == null)
        {
            throw new ArgumentNullException(nameof(sequence));
        }
        if (!sequence.Any())
        {
            throw new ArgumentOutOfRangeException(nameof(sequence));
        }

        var accumulator = startValue;
        foreach (var element in sequence)
        {
            accumulator = transformFunction(element, accumulator);
        }

        return accumulator;
    }
}