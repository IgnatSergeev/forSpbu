namespace StackCalculator;

public class ArrayStack<T> : IStack<T>
{
    public ArrayStack()
    {
        _array = new T[2];
        _headIndex = -1;
    }
    
    public void Pop()
    {
        if (_headIndex == -1)
        {
            throw new ArgumentNullException();
        }

        _array[_headIndex] = default;
        --_headIndex;
    }
    
    public T Top()
    {
        var tmpVar = _array[_headIndex];
        return (_headIndex == -1 || tmpVar == null)
            ? throw new InvalidOperationException("Topping from empty stack")
            : tmpVar;
    }
    
    public void Push(T value)
    {
        if (_headIndex + 1 >= _array.Length)
        {
            Resize();
        }
        ++_headIndex;
        _array[_headIndex] = value;
    }
    
    public void Clear()
    {
        _headIndex = -1;
        _array = new T[2];
    } 
    
    private void Resize()
    {
        var newArray = new T[2 * _array.Length];
        _array.CopyTo(newArray, 0);
        
        _array = newArray;
    }

    public bool IsEmpty()
    {
        return _headIndex == -1;
    }

    private T?[] _array;
    private int _headIndex;
}