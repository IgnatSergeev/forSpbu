namespace UniqueList;

/// <summary>
/// Container that operates with elements by index without equal by value elements
/// </summary>
/// <typeparam name="T">Type of elements in list</typeparam>
public class UniqueList<T> : List<T>
{
    
    /// <summary>
    /// Inserts an element into list by index
    /// </summary>
    /// <param name="valueToAdd">value of new element</param>
    /// <param name="position">index where to insert element</param>
    /// <exception cref="ArgumentNullException">if given value is null</exception>
    /// <exception cref="ArgumentOutOfRangeException">if given index out of range</exception>
    /// <exception cref="ValueAlreadyExistException">if give value already exist in the list</exception>
    public new void Add(T valueToAdd, int position)
    {
        if (_valuesSet.Contains(valueToAdd))
        {
            throw new ValueAlreadyExistException(nameof(valueToAdd));
        }
        base.Add(valueToAdd, position);
        _valuesSet.Add(valueToAdd);
    }

    private readonly HashSet<T> _valuesSet = new ();
}