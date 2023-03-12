using System.Dynamic;

namespace StackCalculator;

/// <summary>
/// LIFO container
/// </summary>
public class Stack<T>
{
    public virtual void Pop() {}
    
    public virtual void Push(T value) {}

    public virtual T Top()
    {
        return (T)Convert.ChangeType(0, typeof(T));
    }

    public virtual void Clear() {}
}