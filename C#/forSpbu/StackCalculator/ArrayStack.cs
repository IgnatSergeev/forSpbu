namespace StackCalculator;

public class ArrayStack<T> : IStack<T>
{
    public ArrayStack()
    {
        _array = new T[2];
        _size = 2;
        _headIndex = -1;
    }
    
    public void Pop()
    {
        if (_headIndex == -1)
        {
            throw new ArgumentNullException();
        }

        --_headIndex;
    }
    
    public T Top()
    {
        return (_headIndex == -1)
            ? throw new ArgumentNullException()
            : _array[_headIndex];
    }
    
    public void Push(T value)
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
    
    public void Clear()
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