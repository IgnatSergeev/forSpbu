using System.Security.Principal;

namespace Game;

public class Game
{
    private Map _map;
    private int _xPosition;
    private int _yPosition;
    private Action<int, int, int, int> _movePlayer;
    
    public enum Side
    {
        Right,
        Left,
        Up,
        Down
    }
    
    public Game(string path, Action<int, int, int, int> movePlayer)
    {
        _map = new Map(path);
        _xPosition = _map.PlayerMapWidth / 2;
        _yPosition = _map.PlayerMapHeight / 2;
        _movePlayer = movePlayer;
    }

    public void OnStart(object? sender, EventArgs args)
    {
        _map.DrawMap();
        Console.SetCursorPosition(0, 0);
    }
    
    public void OnLeft(object? sender, EventArgs args)
    {
        if (_xPosition > 0 && _map.PlayerMap[_yPosition][_xPosition - 1])
        {
            _movePlayer(_xPosition, _yPosition, _xPosition - 1, _yPosition);
            _xPosition--;
        }
    }
    
    public void OnRight(object? sender, EventArgs args)
    {
        if (_xPosition + 1 < _map.PlayerMapWidth && _map.PlayerMap[_yPosition][_xPosition + 1])
        {
            _movePlayer(_xPosition, _yPosition, _xPosition + 1, _yPosition);
            _xPosition++;
        }
    }
    
    public void OnDown(object? sender, EventArgs args)
    {
        if (_yPosition + 1 < _map.PlayerMapHeight && _map.PlayerMap[_yPosition + 1][_xPosition])
        {
            _movePlayer(_xPosition, _yPosition, _xPosition, _yPosition + 1);
            _yPosition++;
        }
    }
    
    public void OnUp(object? sender, EventArgs args)
    {
        if (_yPosition > 0 && _map.PlayerMap[_yPosition - 1][_xPosition])
        {
            _movePlayer(_xPosition, _yPosition, _xPosition, _yPosition - 1);
            _yPosition--;
        }
    }

    public static void OnEnd(object? sender, EventArgs args)
    {
        Console.Clear();
    }
}