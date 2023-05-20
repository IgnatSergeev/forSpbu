namespace FindPare;

public class GameCore
{
    enum State
    {
        firstButtonPressed,
        secondButtonPressed
    }
    public GameCore(int fieldSize)
    {
        _field = new int[fieldSize][];
        for (int i = 0; i < fieldSize; i++)
        {
            _field[i] = new int[fieldSize];
        }
        _isOpened = new bool[fieldSize][];
        for (int i = 0; i < fieldSize; i++)
        {
            _isOpened[i] = new bool[fieldSize];
        }
        _dimensionSize = fieldSize;
        _state = State.secondButtonPressed;
        RandomiseField();
    }

    private void RandomiseField()
    {
        var cellValues = new List<int>(Enumerable.Range(0, _dimensionSize * _dimensionSize / 2));
        cellValues.AddRange(Enumerable.Range(0, _dimensionSize * _dimensionSize / 2));
        var random = new Random();
        for (var i = 0; i < _dimensionSize; i++)
        {
            for (int j = 0; j < _dimensionSize; j++)
            {
                var nextValueIndex = random.Next(cellValues.Count);
                _field[i][j] = cellValues[nextValueIndex];
                cellValues.RemoveAt(nextValueIndex);
            }
        }

    }

    public int GetValue(int firstIndex, int secondIndex)
    {
        if (firstIndex < 0 || firstIndex >= _field.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(firstIndex));
        }

        if (secondIndex < 0 || secondIndex >= _field[0].Length)
        {
            throw new ArgumentOutOfRangeException(nameof(secondIndex));
        }
        return _field[firstIndex][secondIndex];
    }

    public bool IsCellOpened(int firstIndex, int secondIndex)
    {
        if (firstIndex < 0 || firstIndex >= _isOpened.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(firstIndex));
        }

        if (secondIndex < 0 || secondIndex >= _isOpened[0].Length)
        {
            throw new ArgumentOutOfRangeException(nameof(secondIndex));
        }
        return _isOpened[firstIndex][secondIndex];
    }

    public bool OpenCell(int index)
    {
        switch (_state)
        {
            case State.firstButtonPressed:
                int rowIndex = index % _dimensionSize;
                var currentCell = (rowIndex, index - rowIndex * _dimensionSize);
                if (_field[_lastOpened.Item1][_lastOpened.Item2] == _field[currentCell.Item1][currentCell.Item2])
                {
                    return true;
                }

                return false;
            case State.secondButtonPressed:
                int firstIndex = index % _dimensionSize;
                _lastOpened = (firstIndex, index - firstIndex * _dimensionSize);
                return true;
            default:
                throw new ArgumentOutOfRangeException(nameof(_state));
        }
    }

    private State _state;

    private (int, int) _lastOpened;
    private readonly int[][] _field;
    private readonly bool[][] _isOpened;
    private readonly int _dimensionSize;
}