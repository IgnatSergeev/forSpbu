namespace Game;

public class Game
{
    private Map _map;
    private int _xPosition;
    private int _yPosition;

    public Game(string path)
    {
        _map = new Map(path);
        _xPosition = _map._playerMapWidth / 2;
        _yPosition = _map._playerMapHeight / 2;
    }

    public void OnStart(object? sender, EventArgs args)
    {
        _map.DrawMap();
        Console.SetCursorPosition(0, 0);
    }

    public void OnLoopStart(object? sender, EventArgs args)
    {
        
        Console.SetCursorPosition(_xPosition + 1, _yPosition + 1);
        Console.Write('@');
        Console.SetCursorPosition(_xPosition + 1, _yPosition + 1);
    }
    
    public void OnLeft(object? sender, EventArgs args)
    {
        if (_xPosition > 0 && _map._playerMap[_yPosition][_xPosition - 1])
        {
            _xPosition--;
        }
    }
    
    public void OnRight(object? sender, EventArgs args)
    {
        if (_xPosition + 1 < _map._playerMapWidth && _map._playerMap[_yPosition][_xPosition + 1])
        {
            _xPosition++;
        }
    }
    
    public void OnDown(object? sender, EventArgs args)
    {
        if (_yPosition + 1 < _map._playerMapHeight && _map._playerMap[_yPosition + 1][_xPosition])
        {
            _yPosition++;
        }
    }
    
    public void OnUp(object? sender, EventArgs args)
    {
        if (_yPosition > 0 && _map._playerMap[_yPosition - 1][_xPosition])
        {
            _yPosition--;
        }
    }

    public static void OnEnd(object? sender, EventArgs args)
    {
        Console.Clear();
    }
}