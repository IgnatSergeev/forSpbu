namespace StackCalculator;

/// <summary>
/// LIFO container
/// </summary>
public class Stack
{
    public virtual void Pop() {}
    
    public virtual void Push(int value) {}

    public virtual int Top()
    {
        return 0;
    }

    public virtual void Clear() {}
}