namespace UniqueList;

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

    public void Add(T valueToAdd, int position)
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
    
    public void Remove(T valueToAdd, int positionToRemove)
    {
        if (valueToAdd == null)
        {
            throw new ArgumentNullException(nameof(valueToAdd));
        }
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