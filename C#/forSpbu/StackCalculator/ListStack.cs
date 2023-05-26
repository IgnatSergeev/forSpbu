namespace StackCalculator;

public class ListStack<T> : IStack<T>
{
    private class StackElement
    {
        public StackElement(T value, StackElement? next = null)
        {
            Value = value;
            Next = next;
        }

        public T Value { get; }

        public StackElement? Next { get; }
    }

    private StackElement? _head;
    
    public void Push(T value)
    {
        _head = new StackElement(value, _head);
    }
    
    public void Pop()
    {
        _head = (_head == null) ? throw new ArgumentNullException() : _head.Next;
    }
    
    public T Top()
    {
        if (_head == null)
        {
            throw new ArgumentNullException();
        }
        
        return _head.Value;
    }
    
    public void Clear()
    {
        _head = null;
    }

    public bool IsEmpty()
    {
        return _head == null;
    }
}