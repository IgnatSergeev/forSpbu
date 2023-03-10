namespace Stack;

/// <summary>
/// FIFO container
/// </summary>
public class Stack<T>
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

    private StackElement? head;

    public void Push(T value)
    {
        head = (head == null)
            ? new StackElement(value) 
            : new StackElement(value, head);
    }

    public void Pop()
    {
        head = (head == null) ? throw new Exception() : head.Next;
    }

    public T Top()
    {
        if (head == null)
        {
            throw new Exception();
        }
        
        return head.Value;
    }

    public void Clear()
    {
        head = null;
    }
}
