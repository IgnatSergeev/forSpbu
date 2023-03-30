namespace UniqueList;

/// <summary>
/// Container that operates with elements by index
/// </summary>
/// <typeparam name="T">Type of elements in list</typeparam>
public class List<T>
{
    private class Node
    {
        public Node(T value)
        {
            Value = value;
        }
        
        public Node(T value, Node? next)
        {
            Value = value;
            Next = next;
        }
        
        public T Value { get; set; }
        public Node? Next { get; set; }
    }

    /// <summary>
    /// Inserts an element into list by index
    /// </summary>
    /// <param name="valueToAdd">value of new element</param>
    /// <param name="position">index where to insert element</param>
    /// <exception cref="ArgumentNullException">if given value is null</exception>
    /// <exception cref="ArgumentOutOfRangeException">if given index out of range</exception>
    public virtual void Add(T valueToAdd, int position)
    {
        if (valueToAdd == null)
        {
            throw new ArgumentNullException(nameof(valueToAdd));
        }
        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position));
        }
        
        if (position == 0)
        {
            var newHead = new Node(valueToAdd, _head);
            _head = newHead;
            return;
        }
        
        var previousNode = _head;
        for (int currentAddingPosition = 1; currentAddingPosition < position; currentAddingPosition++)
        {
            previousNode = (previousNode != null) ? previousNode.Next : throw new ArgumentOutOfRangeException(nameof(position));
        }

        if (previousNode == null)
        {
            throw new ArgumentOutOfRangeException(nameof(position));
        }
        
        var newNode = new Node(valueToAdd, previousNode.Next);
        previousNode.Next = newNode;
    }
    
    /// <summary>
    /// Removes an element from list by index
    /// </summary>
    /// <param name="positionToRemove">index where to remove element</param>
    /// <exception cref="ArgumentOutOfRangeException">if index is out of range</exception>
    /// <exception cref="ValueDoesntExistException">if value on given index doesnt exist</exception>
    public void Remove(int positionToRemove)
    {
        if (positionToRemove < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(positionToRemove));
        }
        
        if (positionToRemove == 0)
        {
            _head = _head == null ? throw new ValueDoesntExistException(nameof(positionToRemove)) : _head.Next;
            return;
        }
        
        var previousNode = _head;
        for (int currentPosition = 1; currentPosition < positionToRemove; currentPosition++)
        {
            previousNode = (previousNode != null) ? previousNode.Next : throw new ValueDoesntExistException(nameof(positionToRemove));
        }

        if (previousNode?.Next == null)
        {
            throw new ValueDoesntExistException(nameof(positionToRemove));
        }

        previousNode.Next = previousNode.Next.Next;
    }

    /// <summary>
    /// Returns value of the element by the index
    /// </summary>
    /// <param name="position">index of element to get value from</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">if index is out of range</exception>
    /// <exception cref="ValueDoesntExistException">if value on given index doesnt exist</exception>
    public T GetValue(int position)
    {
        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position));
        }

        var currentNode = _head;
        for (int currentPosition = 0; currentPosition < position; currentPosition++)
        {
            currentNode = (currentNode != null) ? currentNode.Next : throw new ValueDoesntExistException(nameof(position));
        }

        return (currentNode != null) ? currentNode.Value : throw new ValueDoesntExistException(nameof(position));
    }

    /// <summary>
    /// Changed elements value by index
    /// </summary>
    /// <param name="newValue">value to assign</param>
    /// <param name="position">index where to set new value</param>
    /// <exception cref="ArgumentNullException">if given value is null</exception>
    /// <exception cref="ArgumentOutOfRangeException">if given index out of range</exception>
    /// <exception cref="ValueDoesntExistException">if value on given index doesnt exist</exception>
    public void ChangeValue(T newValue, int position)
    {
        if (newValue == null)
        {
            throw new ArgumentNullException(nameof(newValue));
        }
        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position));
        }

        var currentNode = _head;
        for (int currentPosition = 0; currentPosition < position; currentPosition++)
        {
            currentNode = (currentNode != null) ? currentNode.Next : throw new ValueDoesntExistException(nameof(position));
        }

        if (currentNode == null)
        {
            throw new ValueDoesntExistException(nameof(position));
        }

        currentNode.Value = newValue;
    }
    
    private Node? _head;
}