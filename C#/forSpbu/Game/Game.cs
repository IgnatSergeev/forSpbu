using System.Security.Principal;

namespace Game;

public class Game
{
    private int _mapWidth;
    private int _mapHeight;
    private int _xPosition;
    private int _yPosition;
    private const string MapPath = "../../../map.txt";
    
    private static void DrawMap()
    {
        Console.Write(File.ReadAllText(MapPath));
    }

    private bool CheckMap()
    {
        using var fileStream = new StreamReader(MapPath);

        var isLastLine = false;
        while (!fileStream.EndOfStream)
        {
            if (isLastLine)
            {
                return false;
            }
            
            var line = fileStream.ReadLine();
            if (string.IsNullOrEmpty(line))
            {
                return false;
            }

            _mapHeight++;
            if (_mapWidth == 0)
            {
                _mapWidth = line.Length;
            }
            else if (_mapWidth != line.Length)
            {
                return false;
            }

            if (_mapHeight != 1 && line[1] == '_')
            {
                isLastLine = true;
            } 
            
            for (var i = 1; i <= _mapWidth; i++)
            {
                var correctSymbol = GetCorrectSymbolByCoordinates(i, _mapHeight, isLastLine);
                if (correctSymbol != line[i - 1])
                {
                    return false;
                }
            }
        }

        _mapHeight -= 2;
        _mapWidth -= 2;
        return true;
    }

    private char GetCorrectSymbolByCoordinates(int xCoordinate, int yCoordinate, bool isLastLine)
    {
        return !isLastLine
            ? yCoordinate == 1
                ? (xCoordinate == 1 || xCoordinate == _mapWidth)
                    ? ' '
                    : '_'
                : (xCoordinate == 1 || xCoordinate == _mapWidth)
                    ? '|'
                    : ' '
            : (xCoordinate == 1 || xCoordinate == _mapWidth)
                ? '|'
                : '_';

    }

    public void OnStart(object? sender, EventArgs args)
    {
        if (!CheckMap())
        {
            throw new InvalidMapException();
        }
        DrawMap();
        _xPosition = _mapWidth / 2;
        _yPosition = _mapHeight / 2;
    }

    public void OnLoopStart(object? sender, EventArgs args)
    {
        Console.SetCursorPosition(_xPosition, _yPosition);
        Console.Write('@');
        Console.SetCursorPosition(_xPosition, _yPosition);
    }
    
    public void OnLeft(object? sender, EventArgs args)
    {
        if (_xPosition > 1)
        {
            _xPosition--;
        }
    }
    
    public void OnRight(object? sender, EventArgs args)
    {
        if (_xPosition < _mapWidth)
        {
            _xPosition++;
        }
    }
    
    public void OnDown(object? sender, EventArgs args)
    {
        if (_yPosition < _mapHeight)
        {
            _yPosition++;
        }
    }
    
    public void OnUp(object? sender, EventArgs args)
    {
        if (_yPosition - 1 > 0)
        {
            _yPosition--;
        }
    }

    public static void OnEnd(object? sender, EventArgs args)
    {
        Console.Clear();
    }
}