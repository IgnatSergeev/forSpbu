namespace StackCalculator;

/// <summary>
/// LIFO container
/// </summary>
public class ListStack<T> : Stack<T>
{
    private class StackElement
    {
        public StackElement(T value, StackElement next)
        {
            Value = value;
            Next = next;
        }
        
        public StackElement(T value)
        {
            Value = value;
        }
        
        public T Value { get; }

        public StackElement? Next { get; }
    }

    private StackElement? _head;

    /// <summary>
    /// Adds an element to the stack head
    /// </summary>
    /// <param name="value">Value to add</param>
    public override void Push(T value)
    {
        _head = (_head == null)
            ? new StackElement(value) 
            : new StackElement(value, _head);
    }

    /// <summary>
    /// Removes an element from the stack head
    /// </summary>
    /// <exception cref="Exception">If pops from empty stack</exception>
    public override void Pop()
    {
        _head = (_head == null) ? throw new Exception("Trying to pop from empty stack") : _head.Next;
    }

    /// <summary>
    /// Return stack`s head element value
    /// </summary>
    /// <returns>Head value</returns>
    /// <exception cref="Exception">If stack is empty</exception>
    public override T Top()
    {
        if (_head == null)
        {
            throw new Exception("Trying to top from empty stack");
        }
        
        return _head.Value;
    }

    /// <summary>
    /// Clears the stack
    /// </summary>
    public override void Clear()
    {
        _head = null;
    }
}