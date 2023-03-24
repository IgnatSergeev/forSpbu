namespace StackCalculator;

/// <summary>
/// LIFO container
/// </summary>
public interface IStack<T>
{
    /// <summary>
    /// Removes an element from the stack head
    /// </summary>
    /// <exception cref="ArgumentNullException">If stack is empty</exception>
    public void Pop();

    /// <summary>
    /// Adds an element to the stack head
    /// </summary>
    /// <param name="value">Value to add</param>
    public void Push(T value);

    /// <summary>
    /// Gets stack`s head element value
    /// </summary>
    /// <returns>Head element value</returns>
    /// <exception cref="ArgumentNullException">If stack is empty</exception>
    public T Top();

    /// <summary>
    /// Clears the stack
    /// </summary>
    public void Clear();
}