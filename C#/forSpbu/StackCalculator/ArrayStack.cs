namespace StackCalculator;

/// <summary>
/// LIFO container
/// </summary>
public class ArrayStack<T> : Stack<T>
{
    public ArrayStack()
    {
        _array = new T[2];
        _size = 2;
        _headIndex = -1;
    }
    
    /// <summary>
    /// Removes an element from the stack head
    /// </summary>
    /// <exception cref="Exception">If pops from empty stack</exception>
    public override void Pop()
    {
        if (_headIndex == -1)
        {
            throw new Exception("Trying to pop from empty stack");
        }

        --_headIndex;
    }

    /// <summary>
    /// Return stack`s head element value
    /// </summary>
    /// <returns>Head value</returns>
    /// <exception cref="Exception">If stack is empty</exception>
    public override T Top()
    {
        return (_headIndex == -1)
            ? throw new Exception("Trying to top from empty stack")
            : _array[_headIndex];
    }

    /// <summary>
    /// Adds an element to the stack head
    /// </summary>
    /// <param name="value">Value to add</param>
    public override void Push(T value)
    {
        if (_headIndex + 1 < _size)
        {
            ++_headIndex;
            _array[_headIndex] = value;
        }
        else
        {
            Resize();
            ++_headIndex;
            _array[_headIndex] = value;
        }
    }

    /// <summary>
    /// Clears the stack
    /// </summary>
    public override void Clear()
    {
        _headIndex = -1;
        _array = new T[2];
        _size = 2;
    } 
    
    private void Resize()
    {
        var newSize = 2 * _size;
        var newArray = new T[newSize];
        _array.CopyTo(newArray, 0);
        
        _array = newArray;
        _size = newSize;
    }

    private T[] _array;
    private int _size;
    private int _headIndex;
}